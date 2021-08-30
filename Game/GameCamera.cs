using UnityEngine;

namespace Master.QSpaceCode.Game
{
    public class GameCamera : MonoBehaviour
    {
        private Transform transformCash;
        private Camera cameraCash;

        public Transform TransformCash => transformCash;
        public Camera Camera => cameraCash;

        private void Awake()
        {
            transformCash = GetComponent<Transform>();
            Core.ViewersKeeper.RegisterGameCamera(this);
            cameraCash = GetComponent<Camera>();
        }
    }
}