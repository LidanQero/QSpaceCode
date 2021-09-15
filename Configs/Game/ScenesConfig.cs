using UnityEngine;

namespace Master.QSpaceCode.Configs.Game
{
    [CreateAssetMenu(fileName = "ScenesConfig", menuName = "Game Configs/Scenes Config", order = 0)]
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