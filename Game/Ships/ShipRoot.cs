using Master.QSpaceCode.Configs.Ships;
using Photon.Pun;
using UnityEngine;

namespace Master.QSpaceCode.Game.Ships
{
    [SelectionBase]
    public sealed class ShipRoot : PunObject
    {
        private ShipShell shell;
        private ShipGenerator generator;
        
        private CharacterController characterController;
        
        private int speedCharacteristic;
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
            Move(Core.UiInputKeeper.MoveVector);
            Rotation(Core.UiInputKeeper.Rotation);
            generator.Update();
        }

        private void Move(Vector2 inputVector)
        {
            var energyCost = 0f;
            
            shell.GetMoveSpeed(speedCharacteristic, out var moveSpeed, out var moveCost);
            shell.GetMarchSpeed(speedCharacteristic, out var marchSpeed, out var marchCost);

            moveSpeed *= Time.deltaTime;
            moveCost *= Time.deltaTime;
            marchSpeed *= Time.deltaTime;
            marchCost *= Time.deltaTime;
            
            energyCost += Mathf.Abs(inputVector.x) * moveCost;
            if (inputVector.y > 0) energyCost += inputVector.y * marchCost;
            else energyCost += Mathf.Abs(inputVector.y) * moveCost;

            generator.SpendEnergy(energyCost, out var canSpend);
            if (!canSpend) inputVector = Vector2.zero;

            var targetMove = new Vector3();
            targetMove += Transform.right * inputVector.x * moveSpeed;
            if (inputVector.y > 0) targetMove += Transform.forward * inputVector.y * marchSpeed;
            else targetMove += Transform.forward * inputVector.y * moveSpeed;

            characterController.Move(targetMove);
            PhotonView.RPC(nameof(SynchronizeJets), RpcTarget.All, inputVector);
        }

        private void Rotation(float input)
        {
            if (input == 0) return;
            
            shell.GetRotateSpeed(speedCharacteristic, out var speed, out var cost);

            speed *= Time.deltaTime;
            cost *= Time.deltaTime;

            generator.SpendEnergy(cost, out var canSpend);
            if (!canSpend) input = 0;
            
            var rotation = Transform.rotation;
            var angle = input * speed;
            rotation *= Quaternion.AngleAxis(angle, Vector3.up);
            Transform.rotation = rotation;
        }

        [PunRPC]
        public void LoadConfig(string config)
        {
            if (shell) Destroy(shell.gameObject);
            
            var newConfig = JsonUtility.FromJson<ShipConfig>(config);
            var newShellConfig = Resources.Load<ShipShellConfig>($"Shells/{newConfig.shell}");
            shell = Instantiate(newShellConfig.ShellPrefab, Transform);
            shell.LoadConfig(newShellConfig);
            
            speedCharacteristic = newShellConfig.SpeedCharacteristic;
            energyLimitCharacteristic = newShellConfig.EnergyRegenCharacteristic;
            energyRegenCharacteristic = newShellConfig.EnergyRegenCharacteristic;
            
            generator = new ShipGenerator();
            generator.SetCharacteristics(energyLimitCharacteristic, energyRegenCharacteristic);
            generator.Reset();
        }

        [PunRPC]
        public void SynchronizeJets(Vector2 value)
        {
            if (shell) shell.UpdateJets(value);
        }
    }
}