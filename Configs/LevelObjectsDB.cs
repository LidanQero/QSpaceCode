using System;
using System.Collections.Generic;
using Master.QSpaceCode.Game;
using UnityEngine;

namespace Master.QSpaceCode.Configs
{
    [CreateAssetMenu(fileName = "LevelObjectsDB", menuName = "Dev Configs/Level Objects DB", order = 0)]
    public sealed class LevelObjectsDB : ScriptableObject
    {
        public List<PrefabStore> prefabsStores;
    }

    [Serializable]
    public struct PrefabStore
    {
        public string prefabName;
        public LevelObject prefabObject;
    }
}