using System.Collections.Generic;
using ExitGames.Client.Photon;
using Master.QSpaceCode.Services.Mediator;
using Master.QSpaceCode.Services.ServicesInterfaces;
using Photon.Pun;
using Photon.Realtime;

namespace Master.QSpaceCode.Services.ServicesClasses
{
    public sealed class PunService : Service, IPunService, IConnectionCallbacks, IMatchmakingCallbacks,
        IInRoomCallbacks, ILobbyCallbacks, IWebRpcCallback, IErrorInfoCallback
    {
        public PunService(ServicesMediator newServicesMediator) : base(newServicesMediator)
        {
        }

        private bool wantToCreateRoom;
        private int wantMaxPlayersInRoom;
        
        public override void InitOnAwake()
        {
            base.InitOnAwake();
            PhotonNetwork.AddCallbackTarget(this);
            servicesMediator.UpdatePunState(PunState.Login);
        }

        public void ConnectToLobby()
        {
            PhotonNetwork.LocalPlayer.NickName = Core.GameInfoKeeper.CurrentLogin;
            PhotonNetwork.ConnectUsingSettings();
            servicesMediator.UpdatePunState(PunState.Other);
        }

        public void CreateWantedRoom()
        {
            wantToCreateRoom = true;
            PhotonNetwork.LeaveLobby();
        }

        public void SetWantedRoomPlayersCount(int count)
        {
            wantMaxPlayersInRoom = count;
        }

        public void ConnectToRoom(string roomName)
        {
            servicesMediator.UpdatePunState(PunState.Other);
            PhotonNetwork.JoinRoom(roomName);
        }

        public void Disconnect()
        {
            PhotonNetwork.Disconnect();
        }

        public void ExitFromRoom()
        {
            servicesMediator.UpdatePunState(PunState.Other);
            PhotonNetwork.LeaveRoom();
        }

        public void CheckPunForGame()
        {
            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.OfflineMode = true;
            }

            if (!PhotonNetwork.InRoom)
            {
                PhotonNetwork.CreateRoom("Offline room");
            }
        }

        public void OnConnected()
        {

        }

        public void OnConnectedToMaster()
        {
            servicesMediator.UpdatePunState(PunState.Other);
            if (!PhotonNetwork.OfflineMode) PhotonNetwork.JoinLobby();
        }

        public void OnDisconnected(DisconnectCause cause)
        {
            if (cause == DisconnectCause.DisconnectByClientLogic ||
                cause == DisconnectCause.None)
            {
                servicesMediator.UpdatePunState(PunState.Login);
            }
            else
            {
                servicesMediator.UpdatePunState(PunState.Other);
                PhotonNetwork.ReconnectAndRejoin();
            }
        }

        public void OnRegionListReceived(RegionHandler regionHandler)
        {

        }

        public void OnCustomAuthenticationResponse(Dictionary<string, object> data)
        {

        }

        public void OnCustomAuthenticationFailed(string debugMessage)
        {

        }

        public void OnFriendListUpdate(List<FriendInfo> friendList)
        {

        }

        public void OnCreatedRoom()
        {
            servicesMediator.UpdatePunState(PunState.ConnectedToRoom);
        }

        public void OnCreateRoomFailed(short returnCode, string message)
        {
            servicesMediator.UpdatePunState(PunState.Other);
            PhotonNetwork.JoinLobby();
        }

        public void OnJoinedRoom()
        {
            servicesMediator.UpdatePunState(PunState.ConnectedToRoom);
        }

        public void OnJoinRoomFailed(short returnCode, string message)
        {
            servicesMediator.UpdatePunState(PunState.Other);
            PhotonNetwork.JoinLobby();
        }

        public void OnJoinRandomFailed(short returnCode, string message)
        {

        }

        public void OnLeftRoom()
        {
            servicesMediator.UpdatePunState(PunState.Other);
        }

        public void OnPlayerEnteredRoom(Player newPlayer)
        {
            
        }

        public void OnPlayerLeftRoom(Player otherPlayer)
        {
            
        }

        public void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            
        }

        public void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            
        }

        public void OnMasterClientSwitched(Player newMasterClient)
        {
            
        }

        public void OnJoinedLobby()
        {
            servicesMediator.UpdatePunState(PunState.ConnectedToLobby);
        }

        public void OnLeftLobby()
        {
            servicesMediator.UpdatePunState(PunState.Other);
            if (wantToCreateRoom)
            {
                wantToCreateRoom = false;
                var options = new RoomOptions
                {
                    MaxPlayers = (byte) wantMaxPlayersInRoom,
                    IsOpen = true,
                    IsVisible = true
                };
                PhotonNetwork.CreateRoom(Core.GameInfoKeeper.CurrentLogin, options);
            }
            else Disconnect();
        }

        public void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            
        }

        public void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
        {

        }

        public void OnWebRpcResponse(OperationResponse response)
        {

        }

        public void OnErrorInfo(ErrorInfo errorInfo)
        {

        }
    }
}