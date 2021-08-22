namespace Master.QSpaceCode.Services.ServicesInterfaces
{
    public interface IPunService
    {
        void GenerateNewLogin();
        void ConnectToLobby();
        void CreateWantedRoom();
        void SetWantedRoomPlayersCount(int count);
        void ConnectToRoom(string roomName);
        void Disconnect();
    }
}