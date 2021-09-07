using Master.QSpaceCode.Game.Ships;
using UnityEngine;

namespace Master.QSpaceCode.Configs.ShipsConfigs
{
    public abstract class ShipGeneratorConfig : ScriptableObject
    {
        [SerializeField] private ShipGenerator generatorPrefab;
        [SerializeField] private float maxEnergy;
        [SerializeField] private float energyRegen;

        public ShipGenerator GeneratorPrefab => generatorPrefab;
        public float MaxEnergy => maxEnergy;
        public float EnergyRegen => energyRegen;
    }
}