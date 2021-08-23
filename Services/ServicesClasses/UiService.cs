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
        public event Action<GameMenuState> ChangeGameMenuStateEvent;
        public bool HasWindowsStack => closeWindowsActions.Count > 0;

        private MainMenuState mainMenuState;
        private MultiplayerMenuState multiplayerMenuState;
        private GameMenuState gameMenuState;

        private readonly Stack<Action> closeWindowsActions = new Stack<Action>();

        public MainMenuState GetMainMenuState() => mainMenuState;
        public MultiplayerMenuState GetMultiplayerMenuState() => multiplayerMenuState;
        public GameMenuState GetGameMenuState() => gameMenuState;

        public void UpdatePunState(PunState punState)
        {
            switch (punState)
            {
                case PunState.Login:
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

        public void OpenGameMain()
        {
            closeWindowsActions.Clear();
            SetGameMenuState(GameMenuState.Main);
        }

        public void OpenGamePause()
        {
            Debug.Log("Ставлю игру на паузу");
            var oldGameMenuState = (GameMenuState) (int) gameMenuState;
            closeWindowsActions.Push(delegate
            {
                SetGameMenuState(oldGameMenuState);
                Debug.Log("Снимаю игру с паузы.");
            });
            SetGameMenuState(GameMenuState.Pause);
        }

        public void OpenGameDisconnect()
        {
            var oldGameMenuState = (GameMenuState) (int) gameMenuState;
            closeWindowsActions.Push(delegate { SetGameMenuState(oldGameMenuState);});
            SetGameMenuState(GameMenuState.Disconnect);
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
            var oldMainMenuState = (MainMenuState) (int) mainMenuState;
            var oldGameMenuState = (GameMenuState) (int) gameMenuState;
            closeWindowsActions.Push(delegate
            {
                servicesMediator.SavePlayerSettings();
                SetMainMenuState(oldMainMenuState);
                SetGameMenuState(oldGameMenuState);
            });
            SetMainMenuState(MainMenuState.GameSettings);
            SetGameMenuState(GameMenuState.GameSettings);
        }

        public void OpenMainMenuGraphicSettings()
        {
            var oldMainMenuState = (MainMenuState) (int) mainMenuState;
            var oldGameMenuState = (GameMenuState) (int) gameMenuState;
            closeWindowsActions.Push(delegate
            {
                servicesMediator.SavePlayerSettings();
                SetMainMenuState(oldMainMenuState);
                SetGameMenuState(oldGameMenuState);
            });
            SetMainMenuState(MainMenuState.GraphicSettings);
            SetGameMenuState(GameMenuState.GraphicSettings);
        }

        public void OpenMainMenuExit()
        {
            var oldMainMenuState = (MainMenuState) (int) mainMenuState;
            var oldGameMenuState = (GameMenuState) (int) gameMenuState;
            closeWindowsActions.Push(delegate
            {
                SetMainMenuState(oldMainMenuState);
                SetGameMenuState(oldGameMenuState);
            });
            SetMainMenuState(MainMenuState.Exit);
            SetGameMenuState(GameMenuState.Exit);
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

        private void SetGameMenuState(GameMenuState newState)
        {
            gameMenuState = newState;
            ChangeGameMenuStateEvent?.Invoke(newState);
        }
    }
}