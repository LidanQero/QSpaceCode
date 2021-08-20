using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Master.QSpaceCode
{
    public class CameraPostProcessingRegister : MonoBehaviour
    {
        private void Awake()
        {
            PostProcessLayer postProcessLayer = GetComponent<PostProcessLayer>();
            PostProcessVolume postProcessVolume = GetComponent<PostProcessVolume>();
            Core.SettingsKeeper.SetCurrentPostProcess(postProcessVolume, postProcessLayer);
        }
    }
}