using UnityEngine;

namespace Master.QSpaceCode.Game
{
    public class GameCamera : MonoBehaviour
    {
        private Transform transformCash;

        public Transform TransformCash => transformCash;

        private void Awake()
        {
            transformCash = GetComponent<Transform>();
            Core.ViewersKeeper.RegisterGameCamera(this);
        }
    }
}