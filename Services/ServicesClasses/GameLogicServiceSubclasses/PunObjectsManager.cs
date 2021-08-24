using Master.QSpaceCode.Game;
using Master.QSpaceCode.Game.Player;
using Photon.Pun;
using UnityEngine;

namespace Master.QSpaceCode.Services.ServicesClasses.GameLogicServiceSubclasses
{
    public class PunObjectsManager
    {
        public ShipRoot PlayerShip { get; private set; }

        public void RegisterPunObject(PunObject punObject)
        {
            switch (punObject)
            {
                case ShipRoot {IsMine: true} playerShip:
                    PlayerShip = playerShip;
                    playerShip.RPC(nameof(playerShip.LoadConfig), RpcTarget.All,
                        Core.ShipsConfig.GetDefaultShipConfig());
                    break;
            }
        }

        public void DeletePunObject(PunObject punObject)
        {
            
        }
    }
}