using System;
using System.Collections.Generic;
using System.Linq;
using ExitGames.Client.Photon;
using Master.QSpaceCode.Services.Mediator;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace Master.QSpaceCode.Services.ServicesClasses.PunCallbackServiceSubclasses
{
    public sealed class PunCallbacksSender : MonoBehaviourPunCallbacks
    {
        public event Action<List<Player>> PlayersUpdateEvent;
        public event Action<List<RoomInfo>> RoomUpdateEvent;

        private readonly List<RoomInfo> rooms = new List<RoomInfo>();
        private readonly List<Player> players = new List<Player>();
        
        private ServicesMediator servicesMediator;
        private bool wantToCreateRoom;
        private int wantMaxPlayersInRoom;

        public void SetMediator(ServicesMediator newMediator)
        {
            servicesMediator = newMediator;
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            servicesMediator.UpdatePunState(PunState.Other);
        }

        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            base.OnCreateRoomFailed(returnCode, message);
            servicesMediator.UpdatePunState(PunState.Other);
            PhotonNetwork.JoinLobby();
        }

        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            base.OnJoinRoomFailed(returnCode, message);
            servicesMediator.UpdatePunState(PunState.Other);
            PhotonNetwork.JoinLobby();
        }

        public override void OnCreatedRoom()
        {
            base.OnCreatedRoom();
            servicesMediator.UpdatePunState(PunState.ConnectedToRoom);
        }

        public override void OnJoinedLobby()
        {
            base.OnJoinedLobby();
            servicesMediator.UpdatePunState(PunState.ConnectedToLobby);
        }

        public override void OnLeftLobby()
        {
            base.OnLeftLobby();
            servicesMediator.UpdatePunState(PunState.Other);
            if (wantToCreateRoom)
            {
                wantToCreateRoom = false;
                RoomOptions options = new RoomOptions
                {
                    MaxPlayers = (byte)wantMaxPlayersInRoom,
                    IsOpen = true,
                    IsVisible = true
                };
                PhotonNetwork.CreateRoom(Core.PunInfoKeeper.GetCurrentLogin(), options);
            }
            else Disconnect();
        }

        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
            if (cause == DisconnectCause.DisconnectByClientLogic)
            {
                servicesMediator.UpdatePunState(PunState.Login);
            }
            else
            {
                servicesMediator.UpdatePunState(PunState.Other);
                PhotonNetwork.ReconnectAndRejoin();
            }
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList)
        {
            base.OnRoomListUpdate(roomList);
            UpdateRoomCash(roomList);
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            UpdatePlayersCash();
            servicesMediator.UpdatePunState(PunState.ConnectedToRoom);
        }

        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            UpdatePlayersCash();
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
            UpdatePlayersCash();
        }

        public override void OnConnectedToMaster()
        {
            base.OnConnectedToMaster();
            servicesMediator.UpdatePunState(PunState.Other);
            if (!PhotonNetwork.OfflineMode) PhotonNetwork.JoinLobby();
        }

        public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            base.OnRoomPropertiesUpdate(propertiesThatChanged);
            servicesMediator.OnRoomPropertiesUpdate(propertiesThatChanged);
        }

        public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            base.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
            servicesMediator.OnPlayerPropertiesUpdate(targetPlayer, changedProps);
        }

        public override void OnMasterClientSwitched(Player newMasterClient)
        {
            base.OnMasterClientSwitched(newMasterClient);
            UpdatePlayersCash();
        }

        public List<RoomInfo> GetRooms() => rooms;
        public List<Player> GetPlayers() => players;
        public string GetRoomName()
        {
            if (PhotonNetwork.InRoom) return PhotonNetwork.CurrentRoom.Name;
            return string.Empty;
        }

        public void CreateWantedRoom()
        {
            wantToCreateRoom = true;
            PhotonNetwork.LeaveLobby();
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

        public void SetWantedRoomPlayersCount(int count) => wantMaxPlayersInRoom = count;

        private void UpdateRoomCash(List<RoomInfo> roomList)
        {
            rooms.Clear();
            rooms.AddRange(from room in roomList
                where room.IsOpen && room.IsVisible && room.MaxPlayers > 0 select room);
            RoomUpdateEvent?.Invoke(rooms);
        }

        private void UpdatePlayersCash()
        {
            players.Clear();
            foreach (var player in PhotonNetwork.PlayerList.ToArray()) players.Add(player);
            PlayersUpdateEvent?.Invoke(players);
        }
    }
}