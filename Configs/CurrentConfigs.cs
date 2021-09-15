using Master.QSpaceCode.Configs.Game;
using Master.QSpaceCode.Configs.Ships;
using UnityEngine;

namespace Master.QSpaceCode.Configs
{
    public static class CurrentConfigs
    {
        private static ScenesConfig scenesConfig;
        private static AudioConfig audioConfig;
        private static UiConfig uiConfig;
        private static GameplayConfig gameplayConfig;
        private static LevelsConfig levelsConfig;
        private static ShipsConfig shipsConfig;

        public static void LoadConfigs()
        {
            scenesConfig = Resources.Load<ScenesConfig>("GameConfigs/ScenesConfig");
            audioConfig = Resources.Load<AudioConfig>("GameConfigs/AudioConfig");
            uiConfig = Resources.Load<UiConfig>("GameConfigs/UiConfig");
            gameplayConfig = Resources.Load<GameplayConfig>("GameConfigs/GameplayConfig");
            levelsConfig = Resources.Load<LevelsConfig>("GameConfigs/LevelsConfig");
            shipsConfig = Resources.Load<ShipsConfig>("GameConfigs/ShipsConfig");
        }
        
        public static ScenesConfig ScenesConfig =>
            scenesConfig ? scenesConfig : Resources.Load<ScenesConfig>("GameConfigs/ScenesConfig");

        public static AudioConfig AudioConfig => 
            audioConfig ? audioConfig : Resources.Load<AudioConfig>("GameConfigs/AudioConfig");

        public static UiConfig UiConfig => 
            uiConfig ? uiConfig : Resources.Load<UiConfig>("GameConfigs/UiConfig");

        public static GameplayConfig GameplayConfig =>
            gameplayConfig ? gameplayConfig : Resources.Load<GameplayConfig>("GameConfigs/GameplayConfig");

        public static LevelsConfig LevelsConfig =>
            levelsConfig ? levelsConfig : Resources.Load<LevelsConfig>("GameConfigs/LevelsConfig");

        public static ShipsConfig ShipsConfig => 
            shipsConfig ? shipsConfig : Resources.Load<ShipsConfig>("GameConfigs/ShipsConfig");
    }
}