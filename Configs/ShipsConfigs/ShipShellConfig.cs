using Master.QSpaceCode.Game.Ships;
using UnityEngine;

namespace Master.QSpaceCode.Configs.ShipsConfigs
{
    public abstract class ShipShellConfig : ScriptableObject
    {
        [SerializeField] private ShipShell shellPrefab;
        [SerializeField] private float baseHealth;
        [SerializeField] private float marchSpeed;
        [SerializeField] private float moveSpeed;
        [SerializeField] private float rotateSpeed;
        [SerializeField] private float marchPowerSpend;
        [SerializeField] private float movePowerSpend;
        [SerializeField] private float rotatePowerSpend;

        public ShipShell ShellPrefab => shellPrefab;
        public float BaseHealth => baseHealth;
        public float MarchSpeed => marchSpeed;
        public float MoveSpeed => moveSpeed;
        public float RotateSpeed => rotateSpeed;
        public float MarchPowerSpend => marchPowerSpend;
        public float MovePowerSpend => movePowerSpend;

        public float RotatePowerSpend => rotatePowerSpend;

        protected virtual void OnValidate()
        {
            
        }
    }
}