using Master.QSpaceCode.Configs;
using Master.QSpaceCode.Configs.Ships;
using Photon.Pun;
using UnityEngine;

namespace Master.QSpaceCode.Game.Ships
{
    [SelectionBase]
    public sealed class ShipRoot : PunObject
    {
        public Vector2 InputMove { get; private set; }
        public float InputRotation { get; private set; }

        private ShipShellConfig shellConfig;
        private ShipShell shell;
        private ShipGenerator generator;
        
        private CharacterController characterController;
        
        private int weaponCharacteristic;
        private int modulesCharacteristic;
        private int speedCharacteristic;
        private int healthCharacteristic;
        private int energyLimitCharacteristic;
        private int energyRegenCharacteristic;

        protected override void Awake()
        {
            base.Awake();
            characterController = GetComponent<CharacterController>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Core.GameInfoKeeper.OnUpdateCharacteristics += UpdateCharacteristics;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Core.GameInfoKeeper.OnUpdateCharacteristics -= UpdateCharacteristics;
        }

        private void Update()
        {
            if (!PhotonView.IsMine || !shell) return;
            InputMove = Core.UiInputKeeper.MoveVector;
            InputRotation = Core.UiInputKeeper.Rotation;
            Move();
            Rotation();
            generator.Update(energyLimitCharacteristic, energyRegenCharacteristic);
            shell.UpdateJets(InputMove, InputRotation);
        }

        private void UpdateCharacteristics()
        {
            if (!shellConfig) return;
            
            var mods = Core.GameInfoKeeper.GetPlayerCharacteristicModifiers(PhotonNetwork.LocalPlayer);

            weaponCharacteristic = shellConfig.WeaponCharacteristic + mods.weaponMod + mods.weaponUpgrade;
            modulesCharacteristic = shellConfig.ModulesCharacteristic + mods.modulesMod + mods.modulesUpgrade;
            healthCharacteristic = shellConfig.HealthCharacteristic + mods.healthMod + mods.healthUpgrade;
            speedCharacteristic = shellConfig.SpeedCharacteristic + mods.speedMod + mods.speedUpgrade;
            energyLimitCharacteristic = shellConfig.EnergyRegenCharacteristic + mods.maxEnergyMod + mods.maxEnUpgrade;
            energyRegenCharacteristic = shellConfig.EnergyRegenCharacteristic + mods.energyRegMod + mods.enRegUpgrade;
        }

        private void Move()
        {
            var energyCost = 0f;

            GetMoveSpeed(speedCharacteristic, out var moveSpeed, out var moveCost);
            GetMarchSpeed(speedCharacteristic, out var marchSpeed, out var marchCost);

            moveSpeed *= Time.deltaTime;
            moveCost *= Time.deltaTime;
            marchSpeed *= Time.deltaTime;
            marchCost *= Time.deltaTime;
            
            energyCost += Mathf.Abs(InputMove.x) * moveCost;
            if (InputMove.y > 0) energyCost += InputMove.y * marchCost;
            else energyCost += Mathf.Abs(InputMove.y) * moveCost;

            generator.SpendEnergy(energyCost, out var canSpend);
            if (!canSpend) InputMove = Vector2.zero;

            var targetMove = new Vector3();
            targetMove += Transform.right * InputMove.x * moveSpeed;
            if (InputMove.y > 0) targetMove += Transform.forward * InputMove.y * marchSpeed;
            else targetMove += Transform.forward * InputMove.y * moveSpeed;

            characterController.Move(targetMove);
        }

        private void Rotation()
        {
            if (InputRotation == 0) return;
            var rotationSpeedMod = InputRotation;
            rotationSpeedMod *= 1 - Mathf.Abs(InputMove.x) / 2;

            GetRotateSpeed(speedCharacteristic, out var speed, out var cost);

            speed *= Time.deltaTime;
            cost *= Time.deltaTime * Mathf.Abs(rotationSpeedMod);

            generator.SpendEnergy(cost, out var canSpend);
            if (!canSpend)
            {
                InputRotation = 0;
                rotationSpeedMod = 0;
            }
            
            var rotation = Transform.rotation;
            var angle = rotationSpeedMod * speed;
            rotation *= Quaternion.AngleAxis(angle, Vector3.up);
            Transform.rotation = rotation;
        }
        
        private void GetMoveSpeed(int characteristic, out float speed, out float powerSpend)
        {
            var config = CurrentConfigs.ShipsConfig;
            var mod = config .ChangeSpeedPerStep * (characteristic - 6);
            speed = (config .BaseSpeed + mod) * shellConfig.MoveSpeedMod;
            powerSpend = config .BaseMoveCost * shellConfig.MovePowerSpendMod;
        }

        private void GetMarchSpeed(int characteristic, out float speed, out float powerSpend)
        {
            var config = CurrentConfigs.ShipsConfig;
            var mod = config.ChangeSpeedPerStep * (characteristic - 6);
            speed = (config.BaseSpeed + mod) * shellConfig.MarchSpeedMod;
            powerSpend = config.BaseMoveCost * shellConfig.MarchPowerSpendMod;
        }

        private void GetRotateSpeed(int characteristic, out float speed, out float powerSpend)
        {
            var config = CurrentConfigs.ShipsConfig;
            var mod = config.ChangeSpeedPerStep * (characteristic - 6);
            speed = (config.BaseSpeed + mod) * shellConfig.RotateSpeedMod;
            powerSpend = config.BaseMoveCost * shellConfig.RotatePowerSpendMod;
        }

        [PunRPC]
        public void LoadConfig(string shipContainer)
        {
            if (shell) Destroy(shell.gameObject);
            if (shellConfig) shellConfig.OnConfigChanged -= UpdateCharacteristics;
            
            var newContainer = JsonUtility.FromJson<ShipContainer>(shipContainer);
            shellConfig = Resources.Load<ShipShellConfig>($"Shells/{newContainer.shell}");
            shell = Instantiate(shellConfig.ShellPrefab, Transform);
            generator = new ShipGenerator();

            shellConfig.OnConfigChanged += UpdateCharacteristics;
            UpdateCharacteristics();
        }
    }
}