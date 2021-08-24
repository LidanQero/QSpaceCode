using System;
using Photon.Pun;
using UnityEngine;

namespace Master.QSpaceCode.Game
{
    [RequireComponent(typeof(PhotonView))]
    public abstract class PunObject : PhotonView
    {
        public Transform TransformCash { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            
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