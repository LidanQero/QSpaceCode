using Master.QSpaceCode.Services.Mediator;
using Master.QSpaceCode.Services.ServicesInterfaces;

namespace Master.QSpaceCode.Services.ServicesClasses
{
    public sealed class InputService : Service, IInputService, IUiInputListener
    {
        public InputService(ServicesMediator newServicesMediator) : base(newServicesMediator)
        {
        }
    }
}