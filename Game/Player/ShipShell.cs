using System.Collections.Generic;
using UnityEngine;

namespace Master.QSpaceCode.Game.Player
{
    public abstract class ShipShell : MonoBehaviour
    {
        [SerializeField] private float baseHealth;
        [SerializeField] private float baseEnergy;
        [SerializeField] private float energyRestore;
        [SerializeField] private float marchSpeed;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float marchPowerSpend;
        [SerializeField] private float movePowerSpend;
        [SerializeField] private float rotateSpeed;
        [Space] [SerializeField] private Transform[] marchJets;
        [SerializeField] private Transform[] leftJets;
        [SerializeField] private Transform[] rightJets;
        [SerializeField] private Transform[] forwardJets;

        private Dictionary<Transform, Vector3> jetsStartScale = new Dictionary<Transform, Vector3>();

        public float BaseHealth => baseHealth;
        public float BaseEnergy => baseEnergy;
        public float EnergyRestore => energyRestore;
        public float MarchSpeed => marchSpeed;
        public float MoveSpeed => moveSpeed;
        public float MarchPowerSpend => marchPowerSpend;
        public float MovePowerSpend => movePowerSpend;
        public float RotateSpeed => rotateSpeed;

        protected ShipWeapon firstWeapon;
        protected ShipWeapon secondWeapon;
        protected ShipWeapon thirdWeapon;
        protected ShipShield shipShield;
        protected ShipCore shipCore;

        public virtual void GetFirstWeapon(out WeaponType weaponType, out ShipWeapon weapon)
        {
            weaponType = WeaponType.None;
            weapon = null;
        }
        
        public virtual void GetSecondWeapon(out WeaponType weaponType, out ShipWeapon weapon)
        {
            weaponType = WeaponType.None;
            weapon = null;
        }
        
        public virtual void GetThirdWeapon(out WeaponType weaponType, out ShipWeapon weapon)
        {
            weaponType = WeaponType.None;
            weapon = null;
        }

        public virtual void LoadConfig(ShipConfig shipConfig)
        {
            foreach (var jet in marchJets) jetsStartScale.Add(jet, jet.localScale);
            foreach (var jet in leftJets) jetsStartScale.Add(jet, jet.localScale);
            foreach (var jet in rightJets) jetsStartScale.Add(jet, jet.localScale);
            foreach (var jet in forwardJets) jetsStartScale.Add(jet, jet.localScale);
        }

        public virtual void UpdateJets(Vector2 moveVector)
        {
            if (moveVector.x > 0)
            {
                WorkWithJets(leftJets, moveVector.x);
                WorkWithJets(rightJets, 0);
            }
            else
            {
                WorkWithJets(rightJets, Mathf.Abs(moveVector.x));
                WorkWithJets(leftJets, 0);
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
        }

        private void WorkWithJets(Transform[] jets, float size)
        {
            foreach (var jet in jets) jet.localScale = jetsStartScale[jet] * size;
        }
    }
}