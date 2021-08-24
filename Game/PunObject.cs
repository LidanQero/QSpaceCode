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

        private void OnEnable()
        {
            Core.ViewersKeeper.RegisterPunObject(this);
        }

        private void OnDisable()
        {
            Core.ViewersKeeper.DeletePunObject(this);
        }
    }
}