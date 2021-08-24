using Photon.Pun;
using UnityEngine;

namespace Master.QSpaceCode.Game.Player
{
    public sealed class ShipRoot : PunObject
    {
        private ShipShell shell;
        private int targetAngle = 0;
        private float lastRotateTime;

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

        private void RotateRight()
        {
            if (!IsMine) return;
            if (!shell) return;
            if (lastRotateTime + 90 / shell.RotateSpeed > Time.time) return;
            lastRotateTime = Time.time;
            if (targetAngle == 0) targetAngle = -90;
            else if (targetAngle == -90) targetAngle = -180;
            else if (targetAngle == -180 || targetAngle == 180) targetAngle = 90;
            else if (targetAngle == 90) targetAngle = 0;
        }

        private void RotateLeft()
        {
            if (!IsMine) return;
            if (!shell) return;
            if (lastRotateTime + 90 / shell.RotateSpeed > Time.time) return;
            lastRotateTime = Time.time;
            if (targetAngle == 0) targetAngle = 90;
            else if (targetAngle == 90) targetAngle = 180;
            else if (targetAngle == 180 || targetAngle == -180) targetAngle = -90;
            else if (targetAngle == -90) targetAngle = 0;
        }

        private void Update()
        {
            if (!shell) return;
            RefreshRotation();
        }

        public void Move(Vector2 inputVector)
        {
            if (!shell) return;
            if (inputVector == Vector2.zero) return;
            var moveVector = inputVector;
            moveVector *= Time.deltaTime;
            var targetPosition = TransformCash.position;
            targetPosition += TransformCash.right * moveVector.x * shell.MoveSpeed;
            if (inputVector.y > 0) targetPosition += TransformCash.forward * moveVector.y * shell.MarchSpeed;
            else targetPosition += TransformCash.forward * moveVector.y * shell.MoveSpeed;
            var max = Core.GameplayConfig.MaxPlayerRangeFromCenter;
            var min = Core.GameplayConfig.MinPlayerRangeFromCenter;
            var sqrCur = targetPosition.sqrMagnitude;
            if (sqrCur > max * max)
                targetPosition = targetPosition.normalized * max;
            else if (sqrCur < min * min)
                targetPosition = targetPosition.normalized * min;
            TransformCash.position = targetPosition;
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

        [PunRPC]
        public void LoadConfig(string config)
        {
            if (shell) Destroy(shell.gameObject);
            var newConfig = JsonUtility.FromJson<ShipConfig>(config);

            if (!newConfig.shell) return;
            shell = Instantiate(newConfig.shell, TransformCash);
            shell.LoadConfig(newConfig);
        }
    }
}