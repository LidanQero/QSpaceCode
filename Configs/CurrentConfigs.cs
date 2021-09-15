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
            scenesConfig = Resources.Load<ScenesConfig>("Configs/ScenesConfig");
            audioConfig = Resources.Load<AudioConfig>("Configs/AudioConfig");
            uiConfig = Resources.Load<UiConfig>("Configs/UiConfig");
            gameplayConfig = Resources.Load<GameplayConfig>("Configs/GameplayConfig");
            levelsConfig = Resources.Load<LevelsConfig>("Configs/LevelsConfig");
            shipsConfig = Resources.Load<ShipsConfig>("Configs/ShipsConfig");
        }
        
        public static ScenesConfig ScenesConfig =>
            scenesConfig ? scenesConfig : Resources.Load<ScenesConfig>("Configs/ScenesConfig");

        public static AudioConfig AudioConfig => 
            audioConfig ? audioConfig : Resources.Load<AudioConfig>("Configs/AudioConfig");

        public static UiConfig UiConfig => 
            uiConfig ? uiConfig : Resources.Load<UiConfig>("Configs/UiConfig");

        public static GameplayConfig GameplayConfig =>
            gameplayConfig ? gameplayConfig : Resources.Load<GameplayConfig>("Configs/GameplayConfig");

        public static LevelsConfig LevelsConfig =>
            levelsConfig ? levelsConfig : Resources.Load<LevelsConfig>("Configs/LevelsConfig");

        public static ShipsConfig ShipsConfig => 
            shipsConfig ? shipsConfig : Resources.Load<ShipsConfig>("Configs/ShipsConfig");
    }
}