using System;
using Photon.Pun;
using UnityEngine;

namespace Master.QSpaceCode.Game.Player
{
    public sealed class ShipRoot : PunObject
    {
        private ShipShell shell;
        private float currentHealth;
        private float currentEnergy;
        private float spendEnergyInFrame;
        private Vector2 lastMoveVector;
        private Vector2 currentMoveVector;
        private float lastEnergyBlockTime;
        private CharacterController characterControllerCash;

        protected override void Awake()
        {
            base.Awake();
            characterControllerCash = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (!shell) return;
            if (!PhotonView.IsMine) return;
            Move(Core.UiInputKeeper.MoveVector);
            Rotation(Core.UiInputKeeper.Rotation);
            RefreshEnergy();
            RefreshJets();
        }

        private void Move(Vector2 inputVector)
        {
            var energyCost = 0f;
            energyCost += Mathf.Abs(inputVector.x) * shell.MovePowerSpend * Time.deltaTime;
            if (inputVector.y > 0) energyCost += inputVector.y * shell.MarchPowerSpend * Time.deltaTime;
            else energyCost += Mathf.Abs(inputVector.y) * shell.MovePowerSpend * Time.deltaTime;
            if (currentEnergy - spendEnergyInFrame < energyCost) inputVector = Vector2.zero;
            
            var targetMove = new Vector3();
            targetMove += TransformCash.right * inputVector.x * shell.MoveSpeed * Time.deltaTime;
            targetMove += TransformCash.forward * inputVector.y * shell.MarchSpeed * Time.deltaTime;
            

            spendEnergyInFrame += energyCost;
            currentMoveVector = inputVector;
            characterControllerCash.Move(targetMove);

            var position = TransformCash.position;
            position.x = Mathf.Clamp(
                position.x, 
                -Core.GameplayConfig.MapSize.x / 2,
                Core.GameplayConfig.MapSize.x / 2);
            position.z = Mathf.Clamp(
                position.z, 
                -Core.GameplayConfig.MapSize.y / 2,
                Core.GameplayConfig.MapSize.y / 2);
            TransformCash.position = position;
        }

        private void Rotation(float input)
        {
            if (input == 0) return;
            var rotation = TransformCash.rotation;
            var angle = input * Time.deltaTime * shell.RotateSpeed;
            rotation *= Quaternion.AngleAxis(angle, Vector3.up);
            TransformCash.rotation = rotation;
        }

        private void RefreshEnergy()
        {
            if (lastEnergyBlockTime + 1 < Time.time)
                spendEnergyInFrame -= shell.EnergyRestore * Time.deltaTime;
            currentEnergy -= spendEnergyInFrame;
            if (spendEnergyInFrame > 0 && currentEnergy < 1) lastEnergyBlockTime = Time.time;
            currentEnergy = Mathf.Clamp(currentEnergy, 0, shell.BaseEnergy);
            spendEnergyInFrame = 0;
        }

        private void RefreshJets()
        {
            if (currentMoveVector != lastMoveVector)
                PhotonView.RPC(nameof(SynchronizeJets), RpcTarget.All, currentMoveVector);
            lastMoveVector = currentMoveVector;
        }

        [PunRPC]
        public void LoadConfig(string config)
        {
            if (shell) Destroy(shell.gameObject);
            var newConfig = JsonUtility.FromJson<ShipConfig>(config);

            if (newConfig.shell == string.Empty) return;
            var newShell = Resources.Load<ShipShell>($"Shells/{newConfig.shell}");
            if (!newShell) return;
            shell = Instantiate(newShell, TransformCash);
            shell.LoadConfig(newConfig);
            currentEnergy = shell.BaseEnergy;
            currentHealth = shell.BaseHealth;
            lastMoveVector = Vector2.one;
        }

        [PunRPC]
        public void SynchronizeJets(Vector2 value)
        {
            if (shell) shell.UpdateJets(value);
        }
    }
}