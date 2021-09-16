using System;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon;
using Master.QSpaceCode.Services.Mediator;
using Master.QSpaceCode.Services.ServicesInterfaces;
using Master.QSpaceCode.Static;
using Photon.Pun;
using Photon.Realtime;
using Random = UnityEngine.Random;

namespace Master.QSpaceCode.Services.ServicesClasses
{
    public sealed class GameInfoService : Service, IGameInfoService, IGameInfoKeeper, ILobbyCallbacks, IInRoomCallbacks,
        IMatchmakingCallbacks
    {
        public GameInfoService(ServicesMediator newServicesMediator) : base(newServicesMediator)
        {
        }

        private readonly string[] playerNamesParts1 =
            {"Blood", "Sky", "Emerald", "Gold", "Night", "Sun", "Abyss", "Phantom"};

        private readonly string[] playerNamesParts2 =
            {"Dragon", "Falcon", "Knight", "Killer", "Ranger", "Death", "Raven", "Warrior"};


        public override void InitOnAwake()
        {
            base.InitOnAwake();
            PhotonNetwork.AddCallbackTarget(this);
            GenerateNewLogin();
        }

        public event Action<string> OnUpdateLogin;
        public event Action<List<Player>> OnPlayersUpdate;
        public event Action<List<RoomInfo>> OnRoomUpdate;

        public string RoomName => PhotonNetwork.InRoom ? PhotonNetwork.CurrentRoom.Name : string.Empty;
        public string CurrentLogin { get; private set; }
        public List<RoomInfo> Rooms { get; } = new List<RoomInfo>();
        public List<Player> Players { get; } = new List<Player>();

        public void GenerateNewLogin()
        {
            CurrentLogin = CreateRandomLogin();
            OnUpdateLogin?.Invoke(CurrentLogin);
        }

        public void OnJoinedLobby()
        {
            UpdatePlayersCash();
        }

        public void OnLeftLobby()
        {
        }

        public void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            UpdateRoomCash(roomList);
        }

        public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
        {
        }

        public void OnPlayerEnteredRoom(Player newPlayer)
        {
            UpdatePlayersCash();
        }

        public void OnPlayerLeftRoom(Player otherPlayer)
        {
            UpdatePlayersCash();
        }

        public void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
        }

        public void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
        }

        public void OnMasterClientSwitched(Player newMasterClient)
        {
            UpdatePlayersCash();
        }

        public void OnFriendListUpdate(List<FriendInfo> friendList)
        {
        }

        public void OnCreatedRoom()
        {
        }

        public void OnCreateRoomFailed(short returnCode, string message)
        {
        }

        public void OnJoinedRoom()
        {
            UpdatePlayersCash();
        }

        public void OnJoinRoomFailed(short returnCode, string message)
        {
        }

        public void OnJoinRandomFailed(short returnCode, string message)
        {
        }

        public void OnLeftRoom()
        {
        }

        private void UpdateRoomCash(List<RoomInfo> roomList)
        {
            Rooms.Clear();
            
            Rooms.AddRange(from room in roomList
                where room.IsOpen && room.IsVisible && room.MaxPlayers > 0
                select room);
            
            OnRoomUpdate?.Invoke(Rooms);
        }

        private void UpdatePlayersCash()
        {
            Players.Clear();
            foreach (var player in PhotonNetwork.PlayerList.ToArray()) Players.Add(player);
            OnPlayersUpdate?.Invoke(Players);
        }

        private string CreateRandomLogin()
        {
            var r1 = Random.Range(0, playerNamesParts1.Length);
            var r2 = Random.Range(0, playerNamesParts2.Length);
            var r3 = Random.Range(0, 60);
            return $"{playerNamesParts1[r1]} {playerNamesParts2[r2]} {StringsStorage.GetTimeString(r3)}";
        }
    }
}