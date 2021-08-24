using System.Linq;
using DG.Tweening;
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
        private GameCamera gameCamera;
        private MinimapCamera minimapCamera;
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
            punObjectsManager.RegisterPunObject(punObject);
        }

        public void DeletePunObject(PunObject punObject)
        {
            punObjectsManager.DeletePunObject(punObject);
        }

        public void RegisterLocalObject(LocalObject localObject)
        {
            localObjectsManager.RegisterLocalObject(localObject);
        }

        public void DeleteLocalObject(LocalObject localObject)
        {
            localObjectsManager.DeleteLocalObject(localObject);
        }

        public void RegisterGameCamera(GameCamera newGameCamera)
        {
            gameCamera = newGameCamera;
        }

        public void RegisterMinimapCamera(MinimapCamera newMinimapCamera)
        {
            minimapCamera = newMinimapCamera;
        }

        private void ClientPrepare()
        {
            SpawnPlayer();
        }

        private void MasterPrepare()
        {
            if (!PhotonNetwork.IsMasterClient) return;
        }

        private void ClientRuntime()
        {
            gameTime += Time.deltaTime;
            punObjectsManager.PlayerShip.Move(Core.UiInputKeeper.MoveVector);
            UpdateCameraPosition();
        }

        private void MasterRuntime()
        {
            if (!PhotonNetwork.IsMasterClient) return;
        }

        private void SpawnPlayer()
        {
            var players = PhotonNetwork.PlayerList.ToArray();
            var playerShip = Core.GameplayConfig.PlayerShip;
            var spawnPos = Vector3.forward * 150;

            var playerNumber = 0;

            for (int i = 0; i < players.Length; i++)
            {
                if (players[i].UserId != PhotonNetwork.LocalPlayer.UserId) continue;
                playerNumber = i;
                break;
            }

            if (players.Length == 2)
            {
                if (playerNumber == 0) spawnPos += Vector3.right * 20;
                else spawnPos += Vector3.left * 20;
            }
            else if (players.Length == 3)
            {
                if (playerNumber == 1) spawnPos += Vector3.right * 40;
                else if (playerNumber == 2) spawnPos += Vector3.left * 40;
            }
            else if (players.Length == 4)
            {
                if (playerNumber == 0) spawnPos += Vector3.right * 20;
                else if (playerNumber == 1) spawnPos += Vector3.right * 60;
                else if (playerNumber == 2) spawnPos += Vector3.left * 20;
                else if (playerNumber == 3) spawnPos += Vector3.left * 60;
            }

            PhotonNetwork.Instantiate(playerShip.name, spawnPos, Quaternion.identity);
        }

        private void UpdateCameraPosition()
        {
            var shipPosition = punObjectsManager.PlayerShip.TransformCash.position;
            var offset = punObjectsManager.PlayerShip.TransformCash.forward * 50;
            var targetPosition = shipPosition + Vector3.up * 20 + offset;
            var max = Core.GameplayConfig.MaxCameraRangeFromCenter;
            if (targetPosition.sqrMagnitude > max * max)
                targetPosition = targetPosition.normalized * max;
            var targetRotation = Quaternion.Euler(90,
                punObjectsManager.PlayerShip.TransformCash.eulerAngles.y, 0);
            gameCamera.TransformCash.DOKill();
            gameCamera.TransformCash.DOMove(targetPosition, 0.5f);
            gameCamera.TransformCash.DORotateQuaternion(targetRotation, 0.5f);
            minimapCamera.TransformCash.DOKill();
            minimapCamera.TransformCash.DORotateQuaternion(targetRotation, 0.5f);
        }
    }
}