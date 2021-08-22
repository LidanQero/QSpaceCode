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

        public static ScenesConfig ScenesConfig =>
            singleton ? singleton.scenesConfig : FindObjectOfType<Core>().scenesConfig;

        public static AudioConfig AudioConfig =>
            singleton ? singleton.audioConfig : FindObjectOfType<Core>().audioConfig;

        public static UiConfig UiConfig =>
            singleton ? singleton.uiConfig : FindObjectOfType<Core>().uiConfig;

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
            }
        }

        private void Start()
        {
            ServicesMediator.Init();
        }

        private void Update()
        {
            ServicesMediator.Runtime();
            UIHelper.UpdateHelper();
        }
    }
}