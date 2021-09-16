using System;
using Master.QSpaceCode.Game;
using UnityEngine;
using UnityEngine.Serialization;

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
        [NonSerialized] public LevelObject prefab;
        public string prefabName;
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;
    }
}