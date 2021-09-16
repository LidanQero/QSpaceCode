using Master.QSpaceCode.Configs;
using UnityEngine;

namespace Master.QSpaceCode.Game.Ships
{
    public sealed class ShipGenerator
    {
        public ShipGenerator()
        {
            currentEnergy = 1000;
        }

        private float currentEnergy;
        private float energyLimit;
        private float energyRegen;
        private float energyChanged;

        public void Update(int limitStat, int regenStat)
        {
            var config = CurrentConfigs.ShipsConfig;
            energyLimit = (limitStat - 6) * config.ChangeEnergyLimitStep + config.BaseEnergyLimit;
            energyRegen = (regenStat - 6) * config.ChangeEnergyRegenStep + config.BaseEnergyRegen;
            energyChanged += energyRegen * Time.deltaTime;
            currentEnergy += energyChanged;
            currentEnergy = Mathf.Clamp(currentEnergy, 0, energyLimit);
            energyChanged = 0;
        }

        public void SpendEnergy(in float value, out bool canSpend)
        {
            canSpend = currentEnergy + energyChanged >= value;
            if (canSpend) energyChanged -= value;
        }
    }
}