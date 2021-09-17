using System.Collections.Generic;
using Master.QSpaceCode.Configs;
using Master.QSpaceCode.Game.Interfaces;
using Photon.Pun;
using UnityEngine;

namespace Master.QSpaceCode.Game.Ships
{
    public abstract class ShipShell : MonoBehaviour, IHitable
    {
        [Space] [SerializeField] private Transform[] marchJets;
        [SerializeField] private Transform[] forwardJets;
        [SerializeField] private Transform[] jets1;
        [SerializeField] private Transform[] jets2;
        [SerializeField] private Transform[] jets3;
        [SerializeField] private Transform[] jets4;

        private readonly Dictionary<Transform, Vector3> jetsStartScale = new Dictionary<Transform, Vector3>();

        private PhotonView photonView;

        private float maxHealth;
        private float currentHealth;

        private void Awake()
        {
            photonView = GetComponentInParent<PhotonView>();
            foreach (var jet in marchJets) jetsStartScale.Add(jet, jet.localScale);
            foreach (var jet in forwardJets) jetsStartScale.Add(jet, jet.localScale);
            foreach (var jet in jets1) jetsStartScale.Add(jet, jet.localScale);
            foreach (var jet in jets2) jetsStartScale.Add(jet, jet.localScale);
            foreach (var jet in jets3) jetsStartScale.Add(jet, jet.localScale);
            foreach (var jet in jets4) jetsStartScale.Add(jet, jet.localScale);
        }
        
        public void Hit(float damage)
        {
            if (!photonView.IsMine) return;
            currentHealth -= damage;
        }

        public void SetMaxHealth(int characteristic)
        {
            var basicHealth = CurrentConfigs.ShipsConfig.BaseHealth;
            var mod = (characteristic - 6) * CurrentConfigs.ShipsConfig.ChangeHealthPerStep;
            var newMaxHealth = basicHealth + mod;
            
            if (maxHealth > 0)
            {
                var percents = newMaxHealth / maxHealth;
                currentHealth *= percents;
            }

            maxHealth = newMaxHealth;
        }

        public void ResetHealth()
        {
            currentHealth = maxHealth;
        }

        public void UpdateJets(Vector2 moveVector, float rotation)
        {
            if (moveVector.x > 0)
            {
                WorkWithJets(jets1, moveVector.x);
                WorkWithJets(jets2, moveVector.x);
                WorkWithJets(jets3, 0);
                WorkWithJets(jets4, 0);
            }
            else
            {
                WorkWithJets(jets1, 0);
                WorkWithJets(jets2, 0);
                WorkWithJets(jets3, Mathf.Abs(moveVector.x));
                WorkWithJets(jets4, Mathf.Abs(moveVector.x));
            }

            if (moveVector.y > 0)
            {
                WorkWithJets(marchJets, moveVector.y);
                WorkWithJets(forwardJets, 0);
            }
            else
            {
                WorkWithJets(forwardJets, Mathf.Abs(moveVector.y));
                WorkWithJets(marchJets, 0);
            }

            if (rotation > 0)
            {
                if (rotation > moveVector.x) WorkWithJets(jets2, rotation);
                if (-rotation < moveVector.x) WorkWithJets(jets4, rotation);
            }
            else if (rotation < 0)
            {
                if (rotation < moveVector.x) WorkWithJets(jets3, -rotation);
                if (-rotation > moveVector.x) WorkWithJets(jets1, -rotation);
            }
        }

        private void WorkWithJets(Transform[] jets, float size)
        {
            foreach (var jet in jets) jet.localScale = jetsStartScale[jet] * size;
        }
    }
}