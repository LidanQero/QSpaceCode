using UnityEngine;

namespace Master.QSpaceCode.Configs
{
    [CreateAssetMenu(fileName = "Scenes Config", menuName = "GameConfigs/Scenes Config", order = 0)]
    public sealed class ScenesConfig : ScriptableObject
    {
        [SerializeField] private string menuSceneName;
        [SerializeField] private string gameSceneName;
        [SerializeField] private string loadingSceneName;

        public string MenuSceneName => menuSceneName;
        public string GameSceneName => gameSceneName;
        public string LoadingSceneName => loadingSceneName;
    }
}