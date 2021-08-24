using UnityEngine;
using UnityEngine.Serialization;

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

        public float MarchSpeed => marchSpeed;
        public float MoveSpeed => moveSpeed;

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
            
        }
    }
}