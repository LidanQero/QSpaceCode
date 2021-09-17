using Master.QSpaceCode.Game.Ships;
using UnityEngine;

namespace Master.QSpaceCode.Configs.Ships
{
    [CreateAssetMenu(fileName = "ShipsConfig", menuName = "Game Configs/Ships Config", order = 0)]
    public class ShipsConfig : ScriptableObject
    {
        [SerializeField] private ShipRoot shipRoot;
        [Space] [SerializeField] private ShipContainer testShipConfig;
        [Space] [SerializeField] private float baseWeapons;
        [SerializeField] private float changeWeaponsPerStep;
        [Space] [SerializeField] private float baseModules;
        [SerializeField] private float changeModulesPerStep;
        [Space] [SerializeField] private float baseHealth;
        [SerializeField] private float changeHealthPerStep;
        [Space] [SerializeField] private float baseSpeed;
        [SerializeField] private float baseMoveCost;
        [SerializeField] private float changeSpeedPerStep;
        [Space] [SerializeField] private float baseEnergyLimit;
        [SerializeField] private float changeEnergyLimitStep;
        [Space] [SerializeField] private float baseEnergyRegen;
        [SerializeField] private float changeEnergyRegenStep;


        public ShipRoot ShipRoot => shipRoot;

        public float BaseWeapons => baseWeapons;

        public float ChangeWeaponsPerStep => changeWeaponsPerStep;

        public float BaseModules => baseModules;

        public float ChangeModulesPerStep => changeModulesPerStep;

        public float BaseHealth => baseHealth;

        public float ChangeHealthPerStep => changeHealthPerStep;
        public float BaseSpeed => baseSpeed;
        public float BaseMoveCost => baseMoveCost;
        public float ChangeSpeedPerStep => changeSpeedPerStep;
        public float BaseEnergyLimit => baseEnergyLimit;
        public float ChangeEnergyLimitStep => changeEnergyLimitStep;

        public float BaseEnergyRegen => baseEnergyRegen;

        public float ChangeEnergyRegenStep => changeEnergyRegenStep;

        public string GetDefaultShipConfig()
        {
            return JsonUtility.ToJson(testShipConfig);
        }
    }
}