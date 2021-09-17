using System;
using Master.QSpaceCode.Game;
using UnityEngine;

namespace Master.QSpaceCode
{
    [Serializable]
    public struct ShipContainer
    {
        public string shell;
    }

    [Serializable]
    public struct LevelContainer
    {
        public string levelName;
        public LevelType levelType;
        public LevelSector levelSector;
        public LevelObjectInfo[] levelObjects;
    }

    [Serializable]
    public struct LevelObjectInfo
    {
        public string prefabName;
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
    }

    public struct PlayerCharacteristicModifiers
    {
        public int weaponMod;
        public int modulesMod;
        public int healthMod;
        public int speedMod;
        public int maxEnergyMod;
        public int energyRegMod;

        public int weaponUpgrade;
        public int modulesUpgrade;
        public int healthUpgrade;
        public int speedUpgrade;
        public int maxEnUpgrade;
        public int enRegUpgrade;
    }
}