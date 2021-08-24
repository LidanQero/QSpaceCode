using System.Collections.Generic;
using Master.QSpaceCode.Game;
using Photon.Pun;
using UnityEngine;

namespace Master.QSpaceCode.Services.ServicesClasses.GameLogicServiceSubclasses
{
    public class LevelManager
    {
        private int chunksSpawned;
        private readonly List<Chunk> chunks = new List<Chunk>();

        public void SpawnStartChunk()
        {
            var nextChunk = Core.LevelsConfig.StartChunk;
            PhotonNetwork.Instantiate(
                $"Chunks/{nextChunk.name}", Vector3.zero, Quaternion.identity);
        }

        public void ManageChunks(float gameTime)
        {
            var completedRange = gameTime * Core.GameplayConfig.GlobalMoveSpeed;
            if (completedRange <= (chunksSpawned - 1) * 40) return;
            if (chunks.Count > 2) PhotonNetwork.Destroy(chunks[0]);
            SpawnNewChunk();
        }

        public void ClearChunks()
        {
            foreach (Chunk chunk in chunks.ToArray())
            {
                if (chunk && chunk.IsMine)
                    PhotonNetwork.Destroy(chunk.gameObject);
            }

            chunksSpawned = 0;
        }

        public void RegisterChunk(Chunk chunk)
        {
            chunksSpawned++;
            chunks.Add(chunk);
        }

        public void DeleteChunk(Chunk chunk)
        {
            chunks.Remove(chunk);
        }

        private void SpawnNewChunk()
        {
            var nextChunk = Core.LevelsConfig.TestChunk;
            PhotonNetwork.InstantiateRoomObject(
                $"Chunks/{nextChunk.name}",
                Vector3.forward * 40 * (chunksSpawned),
                Quaternion.identity);
        }
    }
}