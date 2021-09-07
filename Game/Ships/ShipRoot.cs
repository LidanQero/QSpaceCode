using Master.QSpaceCode.Configs.ShipsConfigs;
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

        protected override void Awake()
        {
            base.Awake();
            characterController = GetComponent<CharacterController>();
        }

        private void Update()
        {
            if (!shell) return;
            if (!PhotonView.IsMine) return;
            Move(Core.UiInputKeeper.MoveVector);
            Rotation(Core.UiInputKeeper.Rotation);
        }

        private void Move(Vector2 inputVector)
        {
            var energyCost = 0f;
            
            energyCost += Mathf.Abs(inputVector.x) * shell.MovePowerSpend * Time.deltaTime;
            if (inputVector.y > 0) energyCost += inputVector.y * shell.MarchPowerSpend * Time.deltaTime;
            else energyCost += Mathf.Abs(inputVector.y) * shell.MovePowerSpend * Time.deltaTime;

            if (!generator.CanSpendEnergy(energyCost)) inputVector = Vector2.zero;
            else generator.ChangeEnergy(-energyCost);
            
            var targetMove = new Vector3();
            targetMove += Transform.right * inputVector.x * shell.MoveSpeed * Time.deltaTime;
            targetMove += Transform.forward * inputVector.y * shell.MarchSpeed * Time.deltaTime;
            
            characterController.Move(targetMove);
            PhotonView.RPC(nameof(SynchronizeJets), RpcTarget.All, inputVector);
        }

        private void Rotation(float input)
        {
            if (input == 0) return;
            var rotation = Transform.rotation;
            var angle = input * Time.deltaTime * shell.RotateSpeed;
            rotation *= Quaternion.AngleAxis(angle, Vector3.up);
            Transform.rotation = rotation;
        }

        [PunRPC]
        public void LoadConfig(string config)
        {
            if (shell) Destroy(shell.gameObject);
            if (generator) Destroy(generator.gameObject);
            
            var newConfig = JsonUtility.FromJson<ShipConfig>(config);

            var newShellConfig = Resources.Load<ShipShellConfig>($"Shells/{newConfig.shell}");
            shell = Instantiate(newShellConfig.ShellPrefab, Transform);
            shell.LoadConfig(newShellConfig);

            var newGeneratorConfig = Resources.Load<ShipGeneratorConfig>($"Generators/{newConfig.shell}");
            generator = Instantiate(newGeneratorConfig.GeneratorPrefab, Transform);
            generator.LoadConfig(newGeneratorConfig);
        }

        [PunRPC]
        public void SynchronizeJets(Vector2 value)
        {
            if (shell) shell.UpdateJets(value);
        }
    }
}