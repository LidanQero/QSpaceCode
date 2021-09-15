using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Master.QSpaceCode.Helpers
{
    public class CameraPostProcessingRegister : MonoBehaviour
    {
        private void Awake()
        {
            var postProcessLayer = GetComponent<PostProcessLayer>();
            var postProcessVolume = GetComponent<PostProcessVolume>();
            Core.SettingsKeeper.SetCurrentPostProcess(postProcessVolume, postProcessLayer);
        }
    }
}