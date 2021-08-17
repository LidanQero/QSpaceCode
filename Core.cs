using Master.QSpaceCode.Configs;
using Master.QSpaceCode.Services.Mediator;
using UnityEngine;

namespace Master.QSpaceCode
{
    public sealed class Core : MonoBehaviour
    {
        [SerializeField] private ScenesConfig scenesConfig;

        public static ScenesConfig ScenesConfig => singleton.scenesConfig;
        
        private static readonly ServicesMediator ServicesMediator = new ServicesMediator();

        public static IGameInfoStorage GameInfoStorage => ServicesMediator.gameInfoStorage;
        public static IPunInfoStorage PunInfoStorage => ServicesMediator.punInfoStorage;
        public static IUiStateKeeper UiStateKeeper => ServicesMediator.uiStateKeeper;
        public static IUiInputListener UiInputListener => ServicesMediator.uiInputListener;
        public static IViewersManager ViewersManager => ServicesMediator.viewersManager;

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
            }
        }

        private void Start()
        {
            ServicesMediator.Init();
        }

        private void Update()
        {
            ServicesMediator.Runtime();
        }
    }
}