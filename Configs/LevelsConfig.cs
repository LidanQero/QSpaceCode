using Master.QSpaceCode.Game;
using UnityEngine;

namespace Master.QSpaceCode.Configs
{
    [CreateAssetMenu(fileName = "Levels Config", menuName = "GameConfigs/Levels Config", order = 0)]
    public class LevelsConfig : ScriptableObject
    {
        [SerializeField] private Chunk startChunk;
        [SerializeField] private Chunk testChunk;

        public Chunk StartChunk => startChunk;
        public Chunk TestChunk => testChunk;
    }
}