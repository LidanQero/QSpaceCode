using System;
using Master.QSpaceCode.Configs;
using Master.QSpaceCode.Services.Mediator;
using UnityEngine;

namespace Master.QSpaceCode
{
    public sealed class Core : MonoBehaviour
    {
        [SerializeField] private ScenesConfig scenesConfig;
        [SerializeField] private AudioConfig audioConfig;
        [SerializeField] private UiConfig uiConfig;
        [SerializeField] private GameplayConfig gameplayConfig;
        [SerializeField] private LevelsConfig levelsConfig;
        [SerializeField] private ShipsConfig shipsConfig;

        public static ScenesConfig ScenesConfig =>
            singleton ? singleton.scenesConfig : FindObjectOfType<Core>().scenesConfig;

        public static AudioConfig AudioConfig =>
            singleton ? singleton.audioConfig : FindObjectOfType<Core>().audioConfig;

        public static UiConfig UiConfig =>
            singleton ? singleton.uiConfig : FindObjectOfType<Core>().uiConfig;
        
        public static GameplayConfig GameplayConfig =>
            singleton ? singleton.gameplayConfig : FindObjectOfType<Core>().gameplayConfig;

        public static LevelsConfig LevelsConfig =>
            singleton ? singleton.levelsConfig : FindObjectOfType<Core>().levelsConfig;

        public static ShipsConfig ShipsConfig =>
            singleton ? singleton.shipsConfig : FindObjectOfType<Core>().shipsConfig;

        private static readonly ServicesMediator ServicesMediator = new ServicesMediator();

        public static IGameInfoKeeper GameInfoKeeper => ServicesMediator.gameInfoKeeper;
        public static ISettingsKeeper SettingsKeeper => ServicesMediator.settingsKeeper;
        public static IPunInfoKeeper PunInfoKeeper => ServicesMediator.punInfoKeeper;
        public static IUiStateKeeper UiStateKeeper => ServicesMediator.uiStateKeeper;
        public static IUiInputKeeper UiInputKeeper => ServicesMediator.uiInputKeeper;
        public static IViewersKeeper ViewersKeeper => ServicesMediator.viewersKeeper;
        public static ISoundsKeeper SoundsKeeper => ServicesMediator.soundsKeeper;

        private static Core singleton;

        public static UiHelper UIHelper { get; } = new UiHelper();

        private void Awake()
        {
            if (!singleton)
            {
                singleton = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                DestroyImmediate(gameObject);
                return;
            }
            
            ServicesMediator.InitOnAwake();
        }

        private void Start()
        {
            ServicesMediator.InitOnStart();
        }

        private void Update()
        {
            ServicesMediator.Runtime();
            UIHelper.UpdateHelper();
        }
    }
}