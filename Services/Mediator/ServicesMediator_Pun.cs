using ExitGames.Client.Photon;
using Photon.Realtime;

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
            punService.GenerateNewLogin();
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

        public void Disconnect()
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

        public void ExitFromRoom()
        {
            punService.ExitFromRoom();
        }

        public void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            scenesService.TestLoadingScenesForPun(targetPlayer, changedProps);
        }
        
        public void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            
        }
    }
}