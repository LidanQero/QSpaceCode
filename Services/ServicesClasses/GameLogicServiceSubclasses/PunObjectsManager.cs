using Master.QSpaceCode.Game;
using Photon.Pun;
using UnityEngine;

namespace Master.QSpaceCode.Services.ServicesClasses.GameLogicServiceSubclasses
{
    public class PunObjectsManager
    {
        private PlayerShip playerShipCash;

        public void MovePlayer(float speed)
        {
            if (playerShipCash) 
                playerShipCash.TransformCash.Translate(Vector3.forward * speed);
        }

        public void DestroyPlayer()
        {
            if (playerShipCash) PhotonNetwork.Destroy(playerShipCash.gameObject);
        }

        public void RegisterPunObject(PunObject punObject)
        {
            switch (punObject)
            {
                case PlayerShip playerShip:
                    if (playerShip.IsMine) playerShipCash = playerShip;
                    break;
            }
        }

        public void DeletePunObject(PunObject punObject)
        {
            
        }
    }
}