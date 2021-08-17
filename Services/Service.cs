using Master.QSpaceCode.Services.Mediator;

namespace Master.QSpaceCode.Services
{
    public abstract class Service
    {
        protected Service(ServicesMediator newServicesMediator)
        {
            servicesMediator = newServicesMediator;
        }

        protected readonly ServicesMediator servicesMediator;
        
        public virtual void Init()
        {
            
        }
        
        public virtual void Runtime()
        {
            
        }
    }
}