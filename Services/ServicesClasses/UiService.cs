using System;
using System.Collections.Generic;
using Master.QSpaceCode.Services.Mediator;
using Master.QSpaceCode.Services.ServicesInterfaces;
using UnityEngine;

namespace Master.QSpaceCode.Services.ServicesClasses
{
    public sealed class UiService : Service, IUiService, IUiStateKeeper
    {
        public UiService(ServicesMediator newServicesMediator) : base(newServicesMediator)
        {
        }

        public event Action<MainMenuState> ChangeMainMenuStateEvent;
        public event Action<MultiplayerMenuState> ChangeMultiplayerMenuStateEvent;

        private MainMenuState mainMenuState;
        private MultiplayerMenuState multiplayerMenuState;

        private readonly Stack<Action> closeWindowsActions = new Stack<Action>();

            public MainMenuState GetMainMenuState() => mainMenuState;
        public MultiplayerMenuState GetMultiplayerMenuState() => multiplayerMenuState;

        public void UpdatePunState(PunState punState)
        {
            switch (punState)
            {
                case PunState.ConnectedToMaster:
                    SetMultiplayerState(MultiplayerMenuState.Login);
                    break;
                case PunState.ConnectedToLobby:
                    SetMultiplayerState(MultiplayerMenuState.Lobby);
                    break;
                case PunState.ConnectedToRoom:
                    SetMultiplayerState(MultiplayerMenuState.Room);
                    break;
                default:
                    SetMultiplayerState(MultiplayerMenuState.Connection);
                    break;
            }
        }

        public void CloseCurrentWindow()
        {
            closeWindowsActions.Pop().Invoke();
        }

        public void OpenMainMenu()
        {
            closeWindowsActions.Clear();
            SetMainMenuState(MainMenuState.Title);
        }

        public void OpenSingleplayer()
        {
            closeWindowsActions.Push(delegate { SetMainMenuState(mainMenuState); });
            SetMainMenuState(MainMenuState.Singleplayer);
        }

        public void OpenMultiplayer()
        {
            closeWindowsActions.Push(delegate { SetMainMenuState(mainMenuState); });
            SetMainMenuState(MainMenuState.Multiplayer);
        }

        public void OpenRoomSettings()
        {
            SetMultiplayerState(MultiplayerMenuState.RoomSettings);
        }

        public void CloseRoomSettings()
        {
            SetMultiplayerState(MultiplayerMenuState.Lobby);
        }

        public void OpenShipEditor()
        {
            closeWindowsActions.Push(delegate { SetMainMenuState(mainMenuState); });
            SetMainMenuState(MainMenuState.ShipEditor);
        }

        private void SetMainMenuState(MainMenuState newState)
        {
            mainMenuState = newState;
            ChangeMainMenuStateEvent?.Invoke(newState);
        }

        private void SetMultiplayerState(MultiplayerMenuState newState)
        {
            multiplayerMenuState = newState;
            ChangeMultiplayerMenuStateEvent?.Invoke(newState);
        }
    }
}