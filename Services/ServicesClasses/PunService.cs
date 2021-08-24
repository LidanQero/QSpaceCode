using System;
using System.Collections.Generic;
using Master.QSpaceCode.Services.Mediator;
using Master.QSpaceCode.Services.ServicesClasses.PunCallbackServiceSubclasses;
using Master.QSpaceCode.Services.ServicesInterfaces;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Master.QSpaceCode.Services.ServicesClasses
{
    public sealed class PunService : Service, IPunService, IPunInfoKeeper
    {
        public PunService(ServicesMediator newServicesMediator) : base(newServicesMediator)
        {
        }

        public event Action<string> UpdateLoginEvent;
        public event Action<List<Player>> PlayersUpdateEvent;
        public event Action<List<RoomInfo>> RoomsUpdateEvent;

        private PunCallbacksSender punCallbacksSender;

        private string currentLogin;

        private readonly string[] playerNamesParts1 =
            {"Blood", "Sky", "Emerald", "Gold", "Night", "Sun", "Abyss", "Phantom"};

        private readonly string[] playerNamesParts2 =
            {"Dragon", "Falcon", "Knight", "Killer", "Ranger", "Death", "Raven", "Warrior"};

        private PunState punState;

        public override void InitOnAwake()
        {
            base.InitOnAwake();

            punCallbacksSender = new GameObject("PunCallbackSender")
                .AddComponent<PunCallbacksSender>();
            punCallbacksSender.SetMediator(servicesMediator);
            Object.DontDestroyOnLoad(punCallbacksSender.gameObject);

            punCallbacksSender.PlayersUpdateEvent += delegate(List<Player> list)
            {
                PlayersUpdateEvent?.Invoke(list);
            };
            punCallbacksSender.RoomUpdateEvent += delegate(List<RoomInfo> list)
            {
                RoomsUpdateEvent?.Invoke(list);
            };

            ChangePunState(PunState.Login);

            GenerateNewLogin();
        }

        public string GetCurrentLogin() => currentLogin;
        public string GetRoomName()
        {
            if (punCallbacksSender) return punCallbacksSender.GetRoomName();
            return string.Empty;
        }
        
        public void GenerateNewLogin()
        {
            currentLogin = CreateRandomLogin();
            UpdateLoginEvent?.Invoke(currentLogin);
        }

        public void ConnectToLobby()
        {
            PhotonNetwork.LocalPlayer.NickName = currentLogin;
            PhotonNetwork.ConnectUsingSettings();
            ChangePunState(PunState.Other);
        }

        public void CreateWantedRoom() => punCallbacksSender.CreateWantedRoom();

        public void SetWantedRoomPlayersCount(int count) =>
            punCallbacksSender.SetWantedRoomPlayersCount(count);

        public void ConnectToRoom(string roomName) =>
            punCallbacksSender.ConnectToRoom(roomName);

        public void Disconnect()
        {
            punCallbacksSender.Disconnect();
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

        public List<RoomInfo> GetRooms()
        {
            if (punCallbacksSender) return punCallbacksSender.GetRooms();
            return new List<RoomInfo>();
        }

        public List<Player> GetPlayers()
        {
            if (punCallbacksSender) return punCallbacksSender.GetPlayers();
            return new List<Player>();
        }

        private string CreateRandomLogin()
        {
            int r1 = Random.Range(0, playerNamesParts1.Length);
            int r2 = Random.Range(0, playerNamesParts2.Length);
            int r3 = Random.Range(0, 60);
            return
                $"{playerNamesParts1[r1]} {playerNamesParts2[r2]} {StringsStorage.GetTimeString(r3)}";
        }

        private void ChangePunState(PunState newState)
        {
            punState = newState;
            servicesMediator.UpdatePunState(newState);
        }
    }
}