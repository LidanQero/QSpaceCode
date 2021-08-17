using Master.QSpaceCode.Services.Mediator;
using Master.QSpaceCode.Services.ServicesInterfaces;

namespace Master.QSpaceCode.Services.ServicesClasses
{
    public sealed class GameLogicService : Service, IGameLogicService, IViewersManager
    {
        public GameLogicService(ServicesMediator newServicesMediator) : base(newServicesMediator)
        {
        }
    }
}