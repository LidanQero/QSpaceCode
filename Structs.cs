using System;
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
        public string[] prefabsNames;
        public Vector3[] positions;
        public Quaternion[] rotations;
        public Vector3[] sizes;
    }
}