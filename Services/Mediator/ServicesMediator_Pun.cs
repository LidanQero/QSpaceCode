using ExitGames.Client.Photon;
using Photon.Realtime;
using UnityEngine;

namespace Master.QSpaceCode.Services.Mediator
{
    public sealed partial class ServicesMediator
    {
        public void ConnectToMaster()
        {
            punService.ConnectToLobby();
        }
        
        public void UpdatePunState(PunState punState)
        {
            uiService.UpdatePunState(punState);
        }

        public void GenerateNewLogin()
        {
            gameInfoService.GenerateNewLogin();
        }

        public void CreateWantedRoom()
        {
            punService.CreateWantedRoom();
        }

        public void SetWantedRoomPlayersCount(int max)
        {
            punService.SetWantedRoomPlayersCount(max);
        }

        public void ConnectToRoom(string roomName)
        {
            punService.ConnectToRoom(roomName);
        }

        public void DisconnectFromLobby()
        {
            punService.Disconnect();
        }

        public void StartMultiplayerGame()
        {
            scenesService.LoadGameScene();
        }

        public void StartSingleplayerGame()
        {
            punService.Disconnect();
            scenesService.LoadGameScene();
        }
        
        public void DisconnectFromGame()
        {
            gameLogicService.ResetGame();
            punService.Disconnect();
            scenesService.LoadMenuScene();
        }

        public void ExitFromRoom()
        {
            punService.ExitFromRoom();
        }
    }
}