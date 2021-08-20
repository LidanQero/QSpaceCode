using System;
using System.Collections.Generic;
using Master.QSpaceCode.Services.Mediator;
using Master.QSpaceCode.Services.ServicesInterfaces;

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

        public void CloseCurrentUiArea()
        {
            closeWindowsActions.Pop().Invoke();
        }

        public void OpenMainMenuTitle()
        {
            closeWindowsActions.Clear();
            SetMainMenuState(MainMenuState.Title);
        }

        public void OpenMainMenuSingleplayer()
        {
            var newState = (MainMenuState) (int) mainMenuState;
            closeWindowsActions.Push(delegate { SetMainMenuState(newState); });
            SetMainMenuState(MainMenuState.Singleplayer);
        }

        public void OpenMainMenuMultiplayer()
        {
            var newState = (MainMenuState) (int) mainMenuState;
            closeWindowsActions.Push(delegate { SetMainMenuState(newState); });
            SetMainMenuState(MainMenuState.Multiplayer);
        }

        public void OpenMainMenuRoomSettings()
        {
            var newState = (MainMenuState) (int) mainMenuState;
            closeWindowsActions.Push(delegate { SetMainMenuState(newState); });
            SetMultiplayerState(MultiplayerMenuState.RoomSettings);
        }

        public void CloseMainMenuRoomSettings()
        {
            SetMultiplayerState(MultiplayerMenuState.Lobby);
        }

        public void OpenMainMenuShipEditor()
        {
            var newState = (MainMenuState) (int) mainMenuState;
            closeWindowsActions.Push(delegate { SetMainMenuState(newState); });
            SetMainMenuState(MainMenuState.ShipEditor);
        }

        public void OpenMainMenuGameSettings()
        {
            var newState = (MainMenuState) (int) mainMenuState;
            closeWindowsActions.Push(delegate
            {
                servicesMediator.SavePlayerSettings();
                SetMainMenuState(newState);
            });
            SetMainMenuState(MainMenuState.GameSettings);
        }

        public void OpenMainMenuGraphicSettings()
        {
            var newState = (MainMenuState) (int) mainMenuState;
            closeWindowsActions.Push(delegate
            {
                servicesMediator.SavePlayerSettings();
                SetMainMenuState(newState);
            });
            SetMainMenuState(MainMenuState.GraphicSettings);
        }

        public void OpenMainMenuExit()
        {
            var newState = (MainMenuState) (int) mainMenuState;
            closeWindowsActions.Push(delegate { SetMainMenuState(newState); });
            SetMainMenuState(MainMenuState.Exit);
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