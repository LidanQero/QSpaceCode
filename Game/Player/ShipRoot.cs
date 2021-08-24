using DG.Tweening;
using Photon.Pun;
using UnityEngine;

namespace Master.QSpaceCode.Game.Player
{
    public sealed class ShipRoot : PunObject
    {
        private ShipShell shell;
        private bool rotating;
        private int targetAngle = 0;

        protected override void Awake()
        {
            base.Awake();
            RefreshRotation();
        }

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
            if (!shell || rotating) return;
            if (targetAngle == 0) targetAngle = -90;
            else if (targetAngle == -90) targetAngle = -180;
            else if (targetAngle == -180 || targetAngle == 180) targetAngle = 90;
            else if (targetAngle == 90) targetAngle = 0;
            rotating = true;
            Quaternion targetRotation = TransformCash.rotation * Quaternion.Euler(0, 90, 0);
            var t = TransformCash.DORotateQuaternion(targetRotation, 0.2f);
            t.onComplete += () =>
            {
                RefreshRotation();
                rotating = false;
            };
        }

        private void RotateLeft()
        {
            if (!shell || rotating) return;
            if (targetAngle == 0) targetAngle = 90;
            else if (targetAngle == 90) targetAngle = 180;
            else if (targetAngle == 180 || targetAngle == -180) targetAngle = -90;
            else if (targetAngle == -90) targetAngle = 0;
            rotating = true;
            Quaternion targetRotation = TransformCash.rotation * Quaternion.Euler(0, -90, 0);
            var t = TransformCash.DORotateQuaternion(targetRotation, 0.2f);
            t.onComplete += () =>
            {
                RefreshRotation();
                rotating = false;
            };
        }

        public void Move(Vector2 vector)
        {
            if (!shell || rotating) return;
            if (vector == Vector2.zero) return; 
            vector *= Time.deltaTime;
            var targetPosition = TransformCash.position;
            targetPosition += TransformCash.right * vector.x * shell.MoveSpeed;
            if (vector.y > 0) targetPosition += TransformCash.forward * vector.y * shell.MarchSpeed;
            else targetPosition += TransformCash.forward * vector.y * shell.MoveSpeed;
            var max = Core.GameplayConfig.MaxPlayerRangeFromCenter;
            var min = Core.GameplayConfig.MinPlayerRangeFromCenter;
            var sqrCur = targetPosition.sqrMagnitude;
            if (sqrCur > max * max)
                targetPosition = targetPosition.normalized * max;
            else if (sqrCur < min * min)
                targetPosition = targetPosition.normalized * min;
            TransformCash.position = targetPosition;
            RefreshRotation();
        }

        private void RefreshRotation()
        {
            var currentAngle =
                Vector3.SignedAngle(TransformCash.forward, TransformCash.position, Vector3.up);
            var dif = currentAngle - targetAngle;
            TransformCash.Rotate(Vector3.up, dif);
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