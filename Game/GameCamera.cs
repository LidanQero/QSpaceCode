using DG.Tweening;
using UnityEngine;

namespace Master.QSpaceCode.Game
{
    public class GameCamera : MonoBehaviour
    {
        private Transform transformCash;
        private Camera camera;
        private float targetCameraSize;

        public Transform TransformCash => transformCash;
        public Camera Camera => camera;

        private void Awake()
        {
            transformCash = GetComponent<Transform>();
            Core.ViewersKeeper.RegisterGameCamera(this);
            camera = GetComponent<Camera>();
            targetCameraSize = camera.orthographicSize;
        }

        private void OnEnable()
        {
            Core.UiInputKeeper.UpCameraSizeEvent += UpSize;
            Core.UiInputKeeper.DownCameraSizeEvent += DownSize;
        }

        private void OnDisable()
        {
            Core.UiInputKeeper.UpCameraSizeEvent -= UpSize;
            Core.UiInputKeeper.DownCameraSizeEvent -= DownSize;
        }

        private void UpSize()
        {
            ChangeSize(Core.GameplayConfig.CameraSizeChangeStep);
        }

        private void DownSize()
        {
            ChangeSize(-Core.GameplayConfig.CameraSizeChangeStep);
        }

        private void ChangeSize(float change)
        {
            targetCameraSize += change;
            targetCameraSize = Mathf.Clamp(targetCameraSize, 
                Core.GameplayConfig.MinCameraSize,
                Core.GameplayConfig.MaxCameraSize);
            camera.DOKill();
            camera.DOOrthoSize(targetCameraSize, 0.5f);
        }
    }
}