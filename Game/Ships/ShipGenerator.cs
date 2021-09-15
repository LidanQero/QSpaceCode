using System;
using Master.QSpaceCode.Configs;
using UnityEngine;

namespace Master.QSpaceCode.Game.Ships
{
    public sealed class ShipGenerator
    {
        public event Action<float> OnEnergyChanged;

        private float currentEnergy;
        private float energyLimit;
        private float energyRegen;
        private float energyChanged;

        public void SetCharacteristics(int limit, int regen)
        {
            var config = CurrentConfigs.ShipsConfig;
            energyLimit = (limit - 6) * config.ChangeEnergyLimitStep + config.BaseEnergyLimit;
            energyRegen = (regen - 6) * config.ChangeEnergyRegenStep + config.BaseEnergyRegen;
        }

        public void Reset()
        {
            currentEnergy = energyLimit;
        }

        public void Update()
        {
            if (energyChanged == 0) return;
            energyChanged += energyRegen * Time.deltaTime;
            currentEnergy += energyChanged;
            currentEnergy = Mathf.Clamp(currentEnergy, 0, energyLimit);
            OnEnergyChanged?.Invoke(currentEnergy);
            energyChanged = 0;
        }

        public void SpendEnergy(in float value, out bool canSpend)
        {
            canSpend = currentEnergy + energyChanged >= value;
            if (canSpend) energyChanged -= value;
        }
    }
}