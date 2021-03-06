using Master.QSpaceCode.Configs;
using Master.QSpaceCode.Services.Mediator;
using Master.QSpaceCode.Static;
using UnityEngine;

namespace Master.QSpaceCode
{
    public sealed class Core : MonoBehaviour
    {
        private static readonly ServicesMediator ServicesMediator = new ServicesMediator();

        public static IGameInfoKeeper GameInfoKeeper => ServicesMediator.gameInfoKeeper;
        public static ISettingsKeeper SettingsKeeper => ServicesMediator.settingsKeeper;
        public static IUiStateKeeper UiStateKeeper => ServicesMediator.uiStateKeeper;
        public static IUiInputKeeper UiInputKeeper => ServicesMediator.uiInputKeeper;
        public static IViewersKeeper ViewersKeeper => ServicesMediator.viewersKeeper;
        public static ISoundsKeeper SoundsKeeper => ServicesMediator.soundsKeeper;

        private static Core singleton;

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
            CurrentConfigs.LoadConfigs();
            ServicesMediator.InitOnStart();
        }

        private void Update()
        {
            ServicesMediator.Runtime();
            UiSelectingController.UpdateHelper();
        }
    }
}