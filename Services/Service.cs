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
        
        public virtual void InitOnAwake()
        {
            
        }

        public virtual void InitOnStart()
        {
            
        }
        
        public virtual void Runtime()
        {
            
        }
    }
}