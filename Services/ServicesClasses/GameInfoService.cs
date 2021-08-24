using ExitGames.Client.Photon;
using Master.QSpaceCode.Services.Mediator;
using Master.QSpaceCode.Services.ServicesInterfaces;
using Photon.Pun;
using UnityEngine;

namespace Master.QSpaceCode.Services.ServicesClasses
{
    public sealed class GameInfoService : Service, IGameInfoService, IGameInfoKeeper
    {
        public GameInfoService(ServicesMediator newServicesMediator) : base(newServicesMediator)
        {
        }
    }
}