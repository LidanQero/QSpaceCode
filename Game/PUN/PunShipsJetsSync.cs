using Master.QSpaceCode.Game.Ships;
using Photon.Pun;
using UnityEngine;

namespace Master.QSpaceCode.Game.PUN
{
    public class PunShipsJetsSync : MonoBehaviourPun, IPunObservable
    {
        private ShipRoot shipRoot;
        private ShipShell shipShell;

        private Vector2 moveVector;
        private float rotation;

        private void Awake()
        {
            shipRoot = GetComponent<ShipRoot>();
            shipShell = GetComponentInChildren<ShipShell>();
        }

        private void Update()
        {
            if (photonView.IsMine) return;
            
            if (shipShell == null)
            {
                shipShell = GetComponentInChildren<ShipShell>();
                return;
            }
            
            Debug.Log($"{moveVector} {rotation}");
            shipShell.UpdateJets(moveVector, rotation);
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsReading)
            {
                moveVector = (Vector2) stream.ReceiveNext();
                rotation = (float) stream.ReceiveNext();
            }
            else
            {
                stream.SendNext(shipRoot.InputMove);
                stream.SendNext(shipRoot.InputRotation);
            }
        }
    }
}