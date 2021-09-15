using UnityEngine;

namespace Master.QSpaceCode.Helpers
{
    public class CameraDynamicViewport : MonoBehaviour
    {
        [SerializeField] private float defaultWidth = 1920;
        [SerializeField] private float defaultHeight = 1080;
        [SerializeField] private float rightPanelSize = 400;

        private Camera cameraCash;

        private void Awake()
        {
            cameraCash = GetComponent<Camera>();
        }

        private void Update()
        {
            var defaultAspect = defaultWidth / defaultHeight;
            float width = Screen.width;
            float height = Screen.height;
            var currentAspect = width / height;
            var dif = defaultAspect / currentAspect;
            var newPanelSize = rightPanelSize * dif;
            var targetWidth = defaultWidth - newPanelSize;
            var targetCamWidth = targetWidth / defaultWidth;

            var cameraCashRect = cameraCash.rect;
            cameraCashRect.xMax = targetCamWidth;
            cameraCash.rect = cameraCashRect;
        }
    }
}