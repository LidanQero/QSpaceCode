using Master.QSpaceCode.Services.Mediator;
using Master.QSpaceCode.Services.ServicesInterfaces;

namespace Master.QSpaceCode.Services.ServicesClasses
{
    public sealed class GameInfoService : Service, IGameInfoService, IGameInfoKeeper
    {
        public GameInfoService(ServicesMediator newServicesMediator) : base(newServicesMediator)
        {
        }
    }
}