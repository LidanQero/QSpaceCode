using Master.QSpaceCode.Game.Ships;
using UnityEngine;

namespace Master.QSpaceCode.Configs.GameConfigs
{
    [CreateAssetMenu(fileName = "Gameplay Config", menuName = "Game Configs/Gameplay Config", order = 0)]
    public class GameplayConfig : ScriptableObject
    {
        [SerializeField] private ShipRoot playerShip;

        public ShipRoot PlayerShip => playerShip;
    }
}