using UnityEngine;

namespace Master.QSpaceCode.Configs
{
    [CreateAssetMenu(fileName = "Ships Config", menuName = "GameConfigs/Ships Config", order = 0)]
    public class ShipsConfig : ScriptableObject
    {
        [SerializeField] private ShipConfig testShipConfig;

        public string GetDefaultShipConfig()
        {
            return JsonUtility.ToJson(testShipConfig);
        }
    }
}