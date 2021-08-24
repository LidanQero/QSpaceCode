using Master.QSpaceCode.Game;
using UnityEngine;

namespace Master.QSpaceCode.Configs
{
    [CreateAssetMenu(fileName = "Gameplay Config", menuName = "GameConfigs/Gameplay Config", order = 0)]
    public class GameplayConfig : ScriptableObject
    {
        [SerializeField] private Vector3 playerSpawnPosition;
        [SerializeField] private float globalMoveSpeed;
        [SerializeField] private PlayerShip playerShip;

        public Vector3 PlayerSpawnPosition => playerSpawnPosition;
        public float GlobalMoveSpeed => globalMoveSpeed;
        public PlayerShip PlayerShip => playerShip;
    }
}