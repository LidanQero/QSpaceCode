using Master.QSpaceCode.Game;
using Master.QSpaceCode.Game.Player;
using UnityEngine;

namespace Master.QSpaceCode.Configs
{
    [CreateAssetMenu(fileName = "Gameplay Config", menuName = "GameConfigs/Gameplay Config", order = 0)]
    public class GameplayConfig : ScriptableObject
    {
        [SerializeField] private ShipRoot playerShip;
        [SerializeField] private float minPlayerRangeFromCenter;
        [SerializeField] private float maxPlayerRangeFromCenter;
        [SerializeField] private float maxCameraRangeFromCenter;
        
        public ShipRoot PlayerShip => playerShip;

        public float MinPlayerRangeFromCenter => minPlayerRangeFromCenter;
        public float MaxPlayerRangeFromCenter => maxPlayerRangeFromCenter;

        public float MaxCameraRangeFromCenter => maxCameraRangeFromCenter;
    }
}