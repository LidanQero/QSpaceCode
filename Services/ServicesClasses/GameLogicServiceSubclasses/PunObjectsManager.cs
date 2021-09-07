using Master.QSpaceCode.Game;
using Master.QSpaceCode.Game.Ships;
using Photon.Pun;

namespace Master.QSpaceCode.Services.ServicesClasses.GameLogicServiceSubclasses
{
    public class PunObjectsManager
    {
        public ShipRoot PlayerShip { get; private set; }

        public void RegisterPunObject(PunObject punObject)
        {
            switch (punObject)
            {
                case ShipRoot playerShip:
                    if (playerShip.PhotonView.IsMine)
                    {
                        PlayerShip = playerShip;
                        playerShip.PhotonView.RPC(nameof(playerShip.LoadConfig), RpcTarget.All,
                            Core.ShipsConfig.GetDefaultShipConfig());
                    }

                    break;
            }
        }

        public void DeletePunObject(PunObject punObject)
        {

        }
    }
}