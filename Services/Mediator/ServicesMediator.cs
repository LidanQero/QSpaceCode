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

            gameInfoKeeper = gameInfoService as IGameInfoKeeper;
            punInfoKeeper = punCallbackService as IPunInfoKeeper;
            uiStateKeeper = uiService as IUiStateKeeper;
            uiInputKeeper = inputService as IUiInputKeeper;
            viewersKeeper = gameLogicService as IViewersKeeper;
        }

        private readonly IGameInfoService gameInfoService;
        private readonly IInputService inputService;
        private readonly IUiService uiService;
        private readonly IGameLogicService gameLogicService;
        private readonly IPunCallbackService punCallbackService;
        private readonly IScenesService scenesService;

        public readonly IGameInfoKeeper gameInfoKeeper;
        public readonly IPunInfoKeeper punInfoKeeper;
        public readonly IUiStateKeeper uiStateKeeper;
        public readonly IUiInputKeeper uiInputKeeper;
        public readonly IViewersKeeper viewersKeeper;

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