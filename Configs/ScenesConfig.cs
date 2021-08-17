using UnityEngine;

namespace Master.QSpaceCode.Configs
{
    [CreateAssetMenu(fileName = "ScenesConfig", menuName = "GameConfigs/ScenesConfig", order = 0)]
    public class ScenesConfig : ScriptableObject
    {
        [SerializeField] private string menuSceneName;
        [SerializeField] private string gameSceneName;
        [SerializeField] private string loadingSceneName;

        public string MenuSceneName => menuSceneName;
        public string GameSceneName => gameSceneName;
        public string LoadingSceneName => loadingSceneName;
    }
}