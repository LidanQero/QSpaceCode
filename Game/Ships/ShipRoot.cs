using Master.QSpaceCode.Configs.Ships;
using Photon.Pun;
using UnityEngine;

namespace Master.QSpaceCode.Game.Ships
{
    [SelectionBase]
    public sealed class ShipRoot : PunObject
    {
        public Vector2 inputMove { get; private set; }
        public float inputRotation { get; private set; }

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

        private void Update()
        {
            if (!PhotonView.IsMine) return;
            if (!shell) return;
            inputMove = Core.UiInputKeeper.MoveVector;
            inputRotation = Core.UiInputKeeper.Rotation;
            Move();
            Rotation();
            generator.Update();
            shell.UpdateJets(inputMove, inputRotation);
        }

        private void Move()
        {
            var energyCost = 0f;

            shell.GetMoveSpeed(speedCharacteristic, out var moveSpeed, out var moveCost);
            shell.GetMarchSpeed(speedCharacteristic, out var marchSpeed, out var marchCost);

            moveSpeed *= Time.deltaTime;
            moveCost *= Time.deltaTime;
            marchSpeed *= Time.deltaTime;
            marchCost *= Time.deltaTime;
            
            energyCost += Mathf.Abs(inputMove.x) * moveCost;
            if (inputMove.y > 0) energyCost += inputMove.y * marchCost;
            else energyCost += Mathf.Abs(inputMove.y) * moveCost;

            generator.SpendEnergy(energyCost, out var canSpend);
            if (!canSpend) inputMove = Vector2.zero;

            var targetMove = new Vector3();
            targetMove += Transform.right * inputMove.x * moveSpeed;
            if (inputMove.y > 0) targetMove += Transform.forward * inputMove.y * marchSpeed;
            else targetMove += Transform.forward * inputMove.y * moveSpeed;

            characterController.Move(targetMove);
        }

        private void Rotation()
        {
            if (inputRotation == 0) return;
            var rotationSpeedMod = inputRotation;
            rotationSpeedMod *= 1 - Mathf.Abs(inputMove.x) / 2;

            shell.GetRotateSpeed(speedCharacteristic, out var speed, out var cost);

            speed *= Time.deltaTime;
            cost *= Time.deltaTime * Mathf.Abs(rotationSpeedMod);

            generator.SpendEnergy(cost, out var canSpend);
            if (!canSpend)
            {
                inputRotation = 0;
                rotationSpeedMod = 0;
            }
            
            var rotation = Transform.rotation;
            var angle = rotationSpeedMod * speed;
            rotation *= Quaternion.AngleAxis(angle, Vector3.up);
            Transform.rotation = rotation;
        }

        [PunRPC]
        public void LoadConfig(string shipContainer)
        {
            if (shell) Destroy(shell.gameObject);
            
            var newContainer = JsonUtility.FromJson<ShipContainer>(shipContainer);
            shellConfig = Resources.Load<ShipShellConfig>($"Shells/{newContainer.shell}");
            shell = Instantiate(shellConfig.ShellPrefab, Transform);
            shell.LoadConfig(shellConfig);

            weaponCharacteristic = shellConfig.WeaponCharacteristic;
            modulesCharacteristic = shellConfig.ModulesCharacteristic;
            healthCharacteristic = shellConfig.HealthCharacteristic;
            speedCharacteristic = shellConfig.SpeedCharacteristic;
            energyLimitCharacteristic = shellConfig.EnergyRegenCharacteristic;
            energyRegenCharacteristic = shellConfig.EnergyRegenCharacteristic;
            
            generator = new ShipGenerator();
            generator.SetCharacteristics(energyLimitCharacteristic, energyRegenCharacteristic);
            generator.Reset();
        }
    }
}