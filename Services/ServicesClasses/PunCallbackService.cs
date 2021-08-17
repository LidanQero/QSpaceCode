using Master.QSpaceCode.Services.Mediator;
using Master.QSpaceCode.Services.ServicesClasses.PunCallbackServiceSubclasses;
using Master.QSpaceCode.Services.ServicesInterfaces;
using UnityEngine;

namespace Master.QSpaceCode.Services.ServicesClasses
{
    public sealed class PunCallbackService : Service, IPunCallbackService, IPunInfoKeeper
    {
        public PunCallbackService(ServicesMediator newServicesMediator) : base(newServicesMediator)
        {
        }
        
        private PunCallbacksSender punCallbacksSender;

        public override void Init()
        {
            base.Init();
            punCallbacksSender = new GameObject("PunCallbackSender")
                .AddComponent<PunCallbacksSender>();
            Object.DontDestroyOnLoad(punCallbacksSender.gameObject);
        }
    }
}