using System.Collections.Generic;
using Master.QSpaceCode.Game;
using UnityEngine;

namespace Master.QSpaceCode.Configs
{
    [CreateAssetMenu(fileName = "LevelObjectsDB", menuName = "Dev Configs/Level Objects DB", order = 0)]
    public sealed class LevelObjectsDB : ScriptableObject
    {
        public List<string> names;
        public List<LevelObject> prefabs;
    }
}