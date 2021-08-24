using System.Linq;
using Master.QSpaceCode.Game;
using Master.QSpaceCode.Services.Mediator;
using Master.QSpaceCode.Services.ServicesClasses.GameLogicServiceSubclasses;
using Master.QSpaceCode.Services.ServicesInterfaces;
using Photon.Pun;
using UnityEngine;

namespace Master.QSpaceCode.Services.ServicesClasses
{
    public sealed class GameLogicService : Service, IGameLogicService, IViewersKeeper
    {
        public GameLogicService(ServicesMediator newServicesMediator) : base(newServicesMediator)
        {
        }

        private readonly LevelManager levelManager = new LevelManager();
        private readonly PunObjectsManager punObjectsManager = new PunObjectsManager();
        private readonly LocalObjectsManager localObjectsManager = new LocalObjectsManager();
        private Transform camera;
        private bool gameIsStart;
        private float gameTime;

        public void PrepareToGame()
        {
            ClientPrepare();
            MasterPrepare();
        }
        
        public void StartGame()
        {
            gameIsStart = true;
        }

        public void ResetGame()
        {
            gameIsStart = false;
            gameTime = 0;
            levelManager.ClearChunks();
            punObjectsManager.DestroyPlayer();
        }

        public override void Runtime()
        {
            base.Runtime();
            if (!gameIsStart) return;
            ClientRuntime();
            MasterRuntime();
        }

        public void RegisterPunObject(PunObject punObject)
        {
            if (punObject is Chunk chunk) levelManager.RegisterChunk(chunk);
            else punObjectsManager.RegisterPunObject(punObject);
        }

        public void DeletePunObject(PunObject punObject)
        {
            if (punObject is Chunk chunk) levelManager.DeleteChunk(chunk);
            else punObjectsManager.DeletePunObject(punObject);
        }

        public void RegisterLocalObject(LocalObject localObject) =>
            localObjectsManager.RegisterLocalObject(localObject);

        public void DeleteLocalObject(LocalObject localObject) =>
            localObjectsManager.DeleteLocalObject(localObject);
        
        private void ClientPrepare()
        {
            camera = Camera.main.transform;
            SpawnPlayer();
        }

        private void MasterPrepare()
        {
            if (!PhotonNetwork.IsMasterClient) return;
            levelManager.SpawnStartChunk();
        }
        
        private void ClientRuntime()
        {
            gameTime += Time.deltaTime;
            var speed = Core.GameplayConfig.GlobalMoveSpeed * Time.deltaTime;
            punObjectsManager.MovePlayer(speed);
            camera.position += Vector3.forward * speed;
        }

        private void MasterRuntime()
        {
            if (!PhotonNetwork.IsMasterClient) return;
            levelManager.ManageChunks(gameTime);
        }
        
        private void SpawnPlayer()
        {
            var players = PhotonNetwork.PlayerList.ToArray();
            var playerShip = Core.GameplayConfig.PlayerShip;
            var spawnPos = Core.GameplayConfig.PlayerSpawnPosition;

            var playerNumber = 0;

            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].UserId != PhotonNetwork.LocalPlayer.UserId) continue;
                playerNumber = i;
                break;
            }

            if (players.Length == 2)
            {
                if (playerNumber == 0) spawnPos += Vector3.right * 2;
                else spawnPos += Vector3.left * 2;
            }
            else if (players.Length == 3)
            {
                if (playerNumber == 1) spawnPos += Vector3.right * 4;
                else if (playerNumber == 2) spawnPos += Vector3.left * 4;
            }
            else if (players.Length == 4)
            {
                if (playerNumber == 0) spawnPos += Vector3.right * 2;
                else if (playerNumber == 1) spawnPos += Vector3.right * 6;
                else if (playerNumber == 2) spawnPos += Vector3.left * 2;
                else if (playerNumber == 3) spawnPos += Vector3.left * 6;
            }

            PhotonNetwork.Instantiate(playerShip.name, spawnPos, Quaternion.identity);
        }
    }
}