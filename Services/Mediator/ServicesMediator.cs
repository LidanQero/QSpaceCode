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
            punService = new PunService(this);
            scenesService = new ScenesService(this);
            soundService = new SoundService(this);
            settingsService = new SettingsService(this);

            services = new[]
            {
                (Service) inputService,
                (Service) gameInfoService,
                (Service) uiService,
                (Service) gameLogicService,
                (Service) punService,
                (Service) scenesService,
                (Service) soundService,
                (Service) settingsService
            };

            gameInfoKeeper = gameInfoService as IGameInfoKeeper;
            punInfoKeeper = punService as IPunInfoKeeper;
            uiStateKeeper = uiService as IUiStateKeeper;
            uiInputKeeper = inputService as IUiInputKeeper;
            viewersKeeper = gameLogicService as IViewersKeeper;
            soundsKeeper = soundService as ISoundsKeeper;
            settingsKeeper = settingsService as ISettingsKeeper;
        }

        private readonly IGameInfoService gameInfoService;
        private readonly IInputService inputService;
        private readonly IUiService uiService;
        private readonly IGameLogicService gameLogicService;
        private readonly IPunService punService;
        private readonly IScenesService scenesService;
        private readonly ISoundService soundService;
        private readonly ISettingsService settingsService;

        public readonly IGameInfoKeeper gameInfoKeeper;
        public readonly ISettingsKeeper settingsKeeper;
        public readonly IPunInfoKeeper punInfoKeeper;
        public readonly IUiStateKeeper uiStateKeeper;
        public readonly IUiInputKeeper uiInputKeeper;
        public readonly IViewersKeeper viewersKeeper;
        public readonly ISoundsKeeper soundsKeeper;

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