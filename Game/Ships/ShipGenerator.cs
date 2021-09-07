using System;
using Master.QSpaceCode.Configs.ShipsConfigs;
using UnityEngine;

namespace Master.QSpaceCode.Game.Ships
{
    public abstract class ShipGenerator : MonoBehaviour
    {
        public event Action<float> OnEnergyChanged;
        
        public float MaxEnergy => shipGeneratorConfig.MaxEnergy;
        public float EnergyRegen => shipGeneratorConfig.EnergyRegen;
        
        protected ShipGeneratorConfig shipGeneratorConfig;

        private float currentEnergy;
        private float energyChanged;

        protected virtual void Update()
        {
            if (energyChanged == 0) return;
            
            currentEnergy += energyChanged;
            currentEnergy = Mathf.Clamp(currentEnergy, 0, shipGeneratorConfig.MaxEnergy);
            OnEnergyChanged?.Invoke(currentEnergy);
            energyChanged = 0;
        }

        public bool CanSpendEnergy(float value)
        {
            return currentEnergy + energyChanged >= value;
        }

        public void ChangeEnergy(float value)
        {
            energyChanged += value;
        }

        public virtual void LoadConfig(ShipGeneratorConfig newConfig)
        {
            shipGeneratorConfig = newConfig;
        }
    }
}