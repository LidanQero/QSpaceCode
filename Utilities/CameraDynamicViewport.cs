using UnityEngine;

namespace Master.QSpaceCode.Utilities
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
            float defaultAspect = defaultWidth / defaultHeight;
            float width = Screen.width;
            float height = Screen.height;
            float currentAspect = width / height;
            float dif = defaultAspect / currentAspect;
            float newPanelSize = rightPanelSize * dif;
            float targetWidth = defaultWidth - newPanelSize;
            float targetCamWidth = targetWidth / defaultWidth;

            Rect cameraCashRect = cameraCash.rect;
            cameraCashRect.xMax = targetCamWidth;
            cameraCash.rect = cameraCashRect;
        }
    }
}