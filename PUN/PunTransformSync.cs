using Photon.Pun;
using UnityEngine;

namespace Master.QSpaceCode.PUN
{
    public class PunTransformSync : MonoBehaviourPun, IPunObservable
    {
        private Transform transform;
        
        private float distance;
        private float angle;

        private Vector3 direction;
        private Vector3 networkPosition;
        private Vector3 storedPosition;

        private Quaternion networkRotation;

        private bool firstTake;

        private void Awake()
        {
            transform = GetComponent<Transform>();
            storedPosition = transform.localPosition;
            networkPosition = Vector3.zero;
            networkRotation = Quaternion.identity;
        }

        private void OnEnable()
        {
            firstTake = true;
        }

        private void Update()
        {
            if (photonView.IsMine) return;
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, networkPosition,
                distance * (1.0f / PhotonNetwork.SerializationRate));
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, networkRotation,
                angle * (1.0f / PhotonNetwork.SerializationRate));
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsReading)
            {
                networkPosition = (Vector3)stream.ReceiveNext();
                direction = (Vector3)stream.ReceiveNext();
                networkRotation = (Quaternion)stream.ReceiveNext();

                if (firstTake)
                {
                    transform.localPosition = networkPosition;
                    transform.localRotation = networkRotation;
                    distance = 0;
                    angle = 0;
                    firstTake = false;
                }
                else
                {
                    var lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
                    networkPosition += direction * lag;
                    distance = Vector3.Distance(transform.localPosition, networkPosition);
                    angle = Quaternion.Angle(transform.localRotation, networkRotation);
                }
            }
            else
            {
                var localPosition = transform.localPosition;
                direction = localPosition - storedPosition;
                storedPosition = localPosition;
                stream.SendNext(localPosition);
                stream.SendNext(direction);
                stream.SendNext(transform.localRotation);
            }
        }
    }
}