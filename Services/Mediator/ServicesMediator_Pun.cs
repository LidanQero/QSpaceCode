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
    }
}