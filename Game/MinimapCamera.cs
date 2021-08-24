using UnityEngine;

namespace Master.QSpaceCode.Game
{
    public class MinimapCamera : MonoBehaviour
    {
        private Transform transformCash;

        public Transform TransformCash => transformCash;

        private void Awake()
        {
            transformCash = GetComponent<Transform>();
            Core.ViewersKeeper.RegisterMinimapCamera(this);
        }
    }
}