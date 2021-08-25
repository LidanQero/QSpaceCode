using Photon.Pun;
using UnityEngine;

namespace Master.QSpaceCode.Game.Player
{
    public sealed class ShipRoot : PunObject
    {
        private ShipShell shell;
        private int targetAngle;
        private float lastRotateTime;
        private float currentHealth;
        private float currentEnergy;
        private float spendEnergyInFrame;
        private Vector2 lastMoveVector;
        private Vector2 currentMoveVector;
        private float lastEnergyBlockTime;

        protected override void OnEnable()
        {
            base.OnEnable();
            Core.UiInputKeeper.RotateShipRightEvent += RotateRight;
            Core.UiInputKeeper.RotateShipLeftEvent += RotateLeft;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Core.UiInputKeeper.RotateShipRightEvent -= RotateRight;
            Core.UiInputKeeper.RotateShipLeftEvent -= RotateLeft;
        }

        private void Update()
        {
            if (!shell) return;
            if (!PhotonView.IsMine) return;
            Move(Core.UiInputKeeper.MoveVector);
            RefreshRotation();
            RefreshEnergy();
            RefreshJets();
        }

        private void Move(Vector2 inputVector)
        {
            var moveVector = inputVector;
            var energyCost = 0f;
            moveVector *= Time.deltaTime;
            var targetPosition = TransformCash.position;
            targetPosition += TransformCash.right * moveVector.x * shell.MoveSpeed;
            energyCost += Mathf.Abs(moveVector.x) * shell.MovePowerSpend;
            if (inputVector.y > 0)
            {
                targetPosition += TransformCash.forward * moveVector.y * shell.MarchSpeed;
                energyCost += moveVector.y * shell.MarchPowerSpend;
            }
            else
            {
                targetPosition += TransformCash.forward * moveVector.y * shell.MoveSpeed;
                energyCost += Mathf.Abs(moveVector.y) * shell.MovePowerSpend;
            }

            if (currentEnergy - spendEnergyInFrame < energyCost)
            {
                currentMoveVector = Vector2.zero;
                return;
            }
            spendEnergyInFrame += energyCost;
            var max = Core.GameplayConfig.MaxPlayerRangeFromCenter;
            var min = Core.GameplayConfig.MinPlayerRangeFromCenter;
            var sqrCur = targetPosition.sqrMagnitude;
            if (sqrCur > max * max)
                targetPosition = targetPosition.normalized * max;
            else if (sqrCur < min * min)
                targetPosition = targetPosition.normalized * min;
            TransformCash.position = targetPosition;
            currentMoveVector = inputVector;
        }

        private void RotateRight()
        {
            if (lastRotateTime + 90 / shell.RotateSpeed > Time.time) return;
            lastRotateTime = Time.time;
            if (targetAngle == 0) targetAngle = -90;
            else if (targetAngle == -90) targetAngle = -180;
            else if (targetAngle == -180 || targetAngle == 180) targetAngle = 90;
            else if (targetAngle == 90) targetAngle = 0;
        }

        private void RotateLeft()
        {
            if (lastRotateTime + 90 / shell.RotateSpeed > Time.time) return;
            lastRotateTime = Time.time;
            if (targetAngle == 0) targetAngle = 90;
            else if (targetAngle == 90) targetAngle = 180;
            else if (targetAngle == 180 || targetAngle == -180) targetAngle = -90;
            else if (targetAngle == -90) targetAngle = 0;
        }

        private void RefreshRotation()
        {
            var currentAngle =
                Vector3.SignedAngle(TransformCash.forward, TransformCash.position, Vector3.up);
            var dif = currentAngle - targetAngle;
            var rotation = TransformCash.rotation;
            var targetRotate = Quaternion.AngleAxis(dif, Vector3.up) * rotation;
            rotation = Quaternion.RotateTowards(rotation, targetRotate,
                    shell.RotateSpeed * Time.deltaTime);
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