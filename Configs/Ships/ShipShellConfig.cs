using Master.QSpaceCode.Game.Ships;
using UnityEngine;

namespace Master.QSpaceCode.Configs.Ships
{
    public abstract class ShipShellConfig : ScriptableObject
    {
        [SerializeField] private ShipShell shellPrefab;
        [Space] [SerializeField] private int speedCharacteristic;
        [SerializeField] private int energyLimitCharacteristic;
        [SerializeField] private int energyRegenCharacteristic;
        [Space] [SerializeField] private float marchSpeedMod;
        [SerializeField] private float moveSpeedMod;
        [SerializeField] private float rotateSpeedMod;
        [Space]
        [SerializeField] private float marchPowerSpendMod;
        [SerializeField] private float movePowerSpendMod;
        [SerializeField] private float rotatePowerSpendMod;

        public ShipShell ShellPrefab => shellPrefab;

        public int SpeedCharacteristic => speedCharacteristic;

        public float MarchSpeedMod => marchSpeedMod;
        public float MoveSpeedMod => moveSpeedMod;
        public float RotateSpeedMod => rotateSpeedMod;
        public float MarchPowerSpendMod => marchPowerSpendMod;
        public float MovePowerSpendMod => movePowerSpendMod;
        public float RotatePowerSpendMod => rotatePowerSpendMod;

        public int EnergyLimitCharacteristic => energyLimitCharacteristic;

        public int EnergyRegenCharacteristic => energyRegenCharacteristic;

        protected virtual void OnValidate()
        {
            
        }
    }
}