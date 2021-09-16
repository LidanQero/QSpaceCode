namespace Master.QSpaceCode.Services.ServicesInterfaces
{
    public interface IPunService
    {
        void ConnectToLobby();
        void CreateWantedRoom();
        void SetWantedRoomPlayersCount(int count);
        void ConnectToRoom(string roomName);
        void Disconnect();
        void ExitFromRoom();
        void CheckPunForGame();
    }
}