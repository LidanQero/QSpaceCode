using Master.QSpaceCode.Game;
using Master.QSpaceCode.Game.Player;
using UnityEngine;

namespace Master.QSpaceCode.Configs
{
    [CreateAssetMenu(fileName = "Gameplay Config", menuName = "GameConfigs/Gameplay Config", order = 0)]
    public class GameplayConfig : ScriptableObject
    {
        [SerializeField] private ShipRoot playerShip;
        [SerializeField] private Vector2 mapSize;

        public ShipRoot PlayerShip => playerShip;
        public Vector2 MapSize => mapSize;
    }
}