using UnityEngine;

namespace Master.QSpaceCode.Configs.GameConfigs
{
    [CreateAssetMenu(fileName = "Ships Config", menuName = "Game Configs/Ships Config", order = 0)]
    public class ShipsConfig : ScriptableObject
    {
        [SerializeField] private ShipConfig testShipConfig;

        public string GetDefaultShipConfig()
        {
            return JsonUtility.ToJson(testShipConfig);
        }
    }
}