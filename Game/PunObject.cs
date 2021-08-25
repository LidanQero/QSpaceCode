using Photon.Pun;
using UnityEngine;

namespace Master.QSpaceCode.Game
{
    [RequireComponent(typeof(PhotonView))]
    public abstract class PunObject : MonoBehaviour
    {
        public Transform TransformCash { get; private set; }
        public PhotonView PhotonView { get; private set; }

        protected virtual void Awake()
        {
            PhotonView = GetComponent<PhotonView>();
            TransformCash = GetComponent<Transform>();
        }

        protected virtual void OnEnable()
        {
            Core.ViewersKeeper.RegisterPunObject(this);
        }

        protected virtual void OnDisable()
        {
            Core.ViewersKeeper.DeletePunObject(this);
        }
    }
}