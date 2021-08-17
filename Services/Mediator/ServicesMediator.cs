using Master.QSpaceCode.Services.ServicesClasses;
using Master.QSpaceCode.Services.ServicesInterfaces;

namespace Master.QSpaceCode.Services.Mediator
{
    public sealed partial class ServicesMediator
    {
        public ServicesMediator()
        {
            gameInfoService = new GameInfoService(this);
            inputService = new InputService(this);
            uiService = new UiService(this);
            gameLogicService = new GameLogicService(this);
            punCallbackService = new PunCallbackService(this);
            scenesService = new ScenesService(this);

            services = new[]
            {
                (Service) inputService,
                (Service) gameInfoService,
                (Service) uiService,
                (Service) gameLogicService,
                (Service) punCallbackService,
                (Service) scenesService
            };

            gameInfoStorage = gameInfoService as IGameInfoStorage;
            punInfoStorage = punCallbackService as IPunInfoStorage;
            uiStateKeeper = uiService as IUiStateKeeper;
            uiInputListener = inputService as IUiInputListener;
            viewersManager = gameLogicService as IViewersManager;
        }

        private readonly IGameInfoService gameInfoService;
        private readonly IInputService inputService;
        private readonly IUiService uiService;
        private readonly IGameLogicService gameLogicService;
        private readonly IPunCallbackService punCallbackService;
        private readonly IScenesService scenesService;

        public readonly IGameInfoStorage gameInfoStorage;
        public readonly IPunInfoStorage punInfoStorage;
        public readonly IUiStateKeeper uiStateKeeper;
        public readonly IUiInputListener uiInputListener;
        public readonly IViewersManager viewersManager;

        private readonly Service[] services;

        public void Init()
        {
            foreach (var service in services)
            {
                service.Init();
            }
        }

        public void Runtime()
        {
            foreach (var service in services)
            {
                service.Runtime();
            }
        }
    }
}