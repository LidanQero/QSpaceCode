using System;
using Master.QSpaceCode.PlayerUi;
using Master.QSpaceCode.PlayerUi.Buttons;
using Master.QSpaceCode.PlayerUi.Dropdowns;
using Master.QSpaceCode.PlayerUi.Sliders;
using Master.QSpaceCode.Services.Mediator;
using Master.QSpaceCode.Services.ServicesInterfaces;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Master.QSpaceCode.Services.ServicesClasses
{
    public sealed class InputService : Service, IInputService, IUiInputKeeper
    {
        public InputService(ServicesMediator newServicesMediator) : base(newServicesMediator)
        {
        }
        
        public SystemInputMap GetSystemInputMap() => currentSystemInputMap;

        public Vector2 MoveVector { get; private set; }
        public float Rotation { get; private set; }

        public event Action<SystemInputMap> ChangeSystemInputMapEvent;
        public event Action InputCancelEvent;
        public event Action InputPauseEvent;

        private SystemInputMap currentSystemInputMap;
        private DefaultInputActions defaultInputActions;
        private GameInputActions gameInputActions;

        public override void InitOnAwake()
        {
            base.InitOnAwake();

            defaultInputActions = new DefaultInputActions();
            defaultInputActions.Enable();

            defaultInputActions.UI.Cancel.performed += delegate
            {
                if (EventSystem.current)
                {
                    if (EventSystem.current.currentSelectedGameObject)
                    {
                        if (EventSystem.current.currentSelectedGameObject
                            .GetComponent<UiDropdownOption>()) return;
                    }
                }

                InputCancelEvent?.Invoke();
            };
            defaultInputActions.UI.Menu.performed += delegate
            {
                if (Core.UiStateKeeper.GetGameMenuState() != GameMenuState.Main) return;
                InputPauseEvent?.Invoke();
            };
            defaultInputActions.SystemMap.MouseActive.performed += delegate
            {
                if (currentSystemInputMap == SystemInputMap.Keyboard) return;
                currentSystemInputMap = SystemInputMap.Keyboard;
                ChangeSystemInputMapEvent?.Invoke(currentSystemInputMap);
            };
            defaultInputActions.SystemMap.KeyboardActive.started += delegate
            {
                if (currentSystemInputMap == SystemInputMap.Keyboard) return;
                currentSystemInputMap = SystemInputMap.Keyboard;
                ChangeSystemInputMapEvent?.Invoke(currentSystemInputMap);
            };
            defaultInputActions.SystemMap.XboxActive.started += delegate
            {
                if (currentSystemInputMap == SystemInputMap.Xbox) return;
                currentSystemInputMap = SystemInputMap.Xbox;
                ChangeSystemInputMapEvent?.Invoke(currentSystemInputMap);
            };

            gameInputActions = new GameInputActions();
            gameInputActions.Enable();
        }

        public override void Runtime()
        {
            base.Runtime();
            MoveVector = gameInputActions.ShipControls.Move.ReadValue<Vector2>();
            Rotation = gameInputActions.ShipControls.Rotation.ReadValue<float>();
        }

        public void AddButton(UiButton uiButton)
        {
            switch (uiButton)
            {
                case BackButton mainMenuBackButton:
                    mainMenuBackButton.ButtonPressedEvent +=
                        servicesMediator.CloseCurrentUiArea;
                    break;
                case MainMenuMultiplayerButton mainMenuMultiplayerButton:
                    mainMenuMultiplayerButton.ButtonPressedEvent +=
                        servicesMediator.OpenMainMenuMultiplayer;
                    break;
                case MainMenuShipEditorButton mainMenuShipEditorButton:
                    mainMenuShipEditorButton.ButtonPressedEvent +=
                        servicesMediator.OpenMainMenuShipEditor;
                    break;
                case MainMenuSingleplayerButton mainMenuSingleplayerButton:
                    mainMenuSingleplayerButton.ButtonPressedEvent +=
                        servicesMediator.OpenMainMenuSingleplayer;
                    break;
                case MainMenuGameSettingsButton mainMenuGameSettingsButton:
                    mainMenuGameSettingsButton.ButtonPressedEvent +=
                        servicesMediator.OpenMainMenuGameSettings;
                    break;
                case MainMenuGraphicSettingsButton mainMenuGraphicSettingsButton:
                    mainMenuGraphicSettingsButton.ButtonPressedEvent +=
                        servicesMediator.OpenMainMenuGraphicSettings;
                    break;
                case MainMenuExitButton mainMenuExitButton:
                    mainMenuExitButton.ButtonPressedEvent +=
                        servicesMediator.OpenMainMenuExit;
                    break;
                case ConfirmExitButton confirmExitButton:
                    confirmExitButton.ButtonPressedEvent +=
                        servicesMediator.Quit;
                    break;
                case MainMenuGenerateNewLoginButton generateNewLoginButton:
                    generateNewLoginButton.ButtonPressedEvent +=
                        servicesMediator.GenerateNewLogin;
                    break;
                case MainMenuLoginButton connectToMasterButton:
                    connectToMasterButton.ButtonPressedEvent +=
                        servicesMediator.ConnectToMaster;
                    break;
                case OpenRoomSettingsButton openRoomSettingsButton:
                    openRoomSettingsButton.ButtonPressedEvent +=
                        servicesMediator.OpenMainMenuRoomSettings;
                    break;
                case MainMenuCancelRoomSettingsButton cancelRoomSettingsButton:
                    cancelRoomSettingsButton.ButtonPressedEvent +=
                        servicesMediator.CloseMainMenuRoomSettings;
                    break;
                case RoomButton roomButton:
                    roomButton.UseRoomButtonEvent += TryConnectToRoom;
                    break;
                case MainMenuDisconnectButton menuDisconnectButton:
                    menuDisconnectButton.ButtonPressedEvent +=
                        servicesMediator.DisconnectFromLobby;
                    break;
                case MainMenuCreateRoomButton createRoomButton:
                    createRoomButton.ButtonPressedEvent +=
                        servicesMediator.CreateWantedRoom;
                    break;
                case MainMenuStartMultiplayerGameButton mainMenuStartMultiplayerGameButton:
                    mainMenuStartMultiplayerGameButton.ButtonPressedEvent +=
                        servicesMediator.StartMultiplayerGame;
                    break;
                case MainMenuExitFromRoomButton mainMenuExitFromRoomButton:
                    mainMenuExitFromRoomButton.ButtonPressedEvent +=
                        servicesMediator.ExitFromRoom;
                    break;
                case MainMenuStartSingleplayerButton mainMenuStartSingleplayerButton:
                    mainMenuStartSingleplayerButton.ButtonPressedEvent +=
                        servicesMediator.StartSingleplayerGame;
                    break;
                case GameOpenDisconnectWindowButton openDisconnectWindowButton:
                    openDisconnectWindowButton.ButtonPressedEvent +=
                        servicesMediator.OpenDisconnectWindow;
                    break;
                case GameDisconnectButton disconnectButton:
                    disconnectButton.ButtonPressedEvent +=
                        servicesMediator.DisconnectFromGame;
                    break;
                case GamePauseButton pauseButton:
                    pauseButton.ButtonPressedEvent +=
                        servicesMediator.OpenPauseWindow;
                    break;
            }
        }

        public void RemoveButton(UiButton uiButton)
        {
            switch (uiButton)
            {
                case BackButton mainMenuBackButton:
                    mainMenuBackButton.ButtonPressedEvent -=
                        servicesMediator.CloseCurrentUiArea;
                    break;
                case MainMenuMultiplayerButton mainMenuMultiplayerButton:
                    mainMenuMultiplayerButton.ButtonPressedEvent -=
                        servicesMediator.OpenMainMenuMultiplayer;
                    break;
                case MainMenuShipEditorButton mainMenuShipEditorButton:
                    mainMenuShipEditorButton.ButtonPressedEvent -=
                        servicesMediator.OpenMainMenuShipEditor;
                    break;
                case MainMenuSingleplayerButton mainMenuSingleplayerButton:
                    mainMenuSingleplayerButton.ButtonPressedEvent -=
                        servicesMediator.OpenMainMenuSingleplayer;
                    break;
                case MainMenuGameSettingsButton mainMenuGameSettingsButton:
                    mainMenuGameSettingsButton.ButtonPressedEvent -=
                        servicesMediator.OpenMainMenuGameSettings;
                    break;
                case MainMenuGraphicSettingsButton mainMenuGraphicSettingsButton:
                    mainMenuGraphicSettingsButton.ButtonPressedEvent -=
                        servicesMediator.OpenMainMenuGraphicSettings;
                    break;
                case MainMenuExitButton mainMenuExitButton:
                    mainMenuExitButton.ButtonPressedEvent -=
                        servicesMediator.OpenMainMenuExit;
                    break;
                case ConfirmExitButton confirmExitButton:
                    confirmExitButton.ButtonPressedEvent -=
                        servicesMediator.Quit;
                    break;
                case MainMenuGenerateNewLoginButton generateNewLoginButton:
                    generateNewLoginButton.ButtonPressedEvent -=
                        servicesMediator.GenerateNewLogin;
                    break;
                case MainMenuLoginButton connectToMasterButton:
                    connectToMasterButton.ButtonPressedEvent -=
                        servicesMediator.ConnectToMaster;
                    break;
                case OpenRoomSettingsButton openRoomSettingsButton:
                    openRoomSettingsButton.ButtonPressedEvent -=
                        servicesMediator.OpenMainMenuRoomSettings;
                    break;
                case MainMenuCancelRoomSettingsButton cancelRoomSettingsButton:
                    cancelRoomSettingsButton.ButtonPressedEvent -=
                        servicesMediator.CloseMainMenuRoomSettings;
                    break;
                case RoomButton roomButton:
                    roomButton.UseRoomButtonEvent -= TryConnectToRoom;
                    break;
                case MainMenuDisconnectButton menuDisconnectButton:
                    menuDisconnectButton.ButtonPressedEvent -=
                        servicesMediator.DisconnectFromLobby;
                    break;
                case MainMenuCreateRoomButton createRoomButton:
                    createRoomButton.ButtonPressedEvent -=
                        servicesMediator.CreateWantedRoom;
                    break;
                case MainMenuStartMultiplayerGameButton mainMenuStartMultiplayerGameButton:
                    mainMenuStartMultiplayerGameButton.ButtonPressedEvent -=
                        servicesMediator.StartMultiplayerGame;
                    break;
                case MainMenuExitFromRoomButton mainMenuExitFromRoomButton:
                    mainMenuExitFromRoomButton.ButtonPressedEvent -=
                        servicesMediator.ExitFromRoom;
                    break;
                case MainMenuStartSingleplayerButton mainMenuStartSingleplayerButton:
                    mainMenuStartSingleplayerButton.ButtonPressedEvent -=
                        servicesMediator.StartSingleplayerGame;
                    break;
                case GameOpenDisconnectWindowButton openDisconnectWindowButton:
                    openDisconnectWindowButton.ButtonPressedEvent -=
                        servicesMediator.OpenDisconnectWindow;
                    break;
                case GameDisconnectButton disconnectButton:
                    disconnectButton.ButtonPressedEvent -=
                        servicesMediator.DisconnectFromGame;
                    break;
                case GamePauseButton pauseButton:
                    pauseButton.ButtonPressedEvent -=
                        servicesMediator.OpenPauseWindow;
                    break;
            }
        }

        private void TryConnectToRoom(RoomButton roomButton)
        {
            servicesMediator.ConnectToRoom(roomButton.RoomName);
        }

        public void AddSlider(UiSlider uiSlider)
        {
            switch (uiSlider)
            {
                case MusicVolumeSlider musicVolumeSlider:
                    musicVolumeSlider.SliderChangedValueEvent +=
                        servicesMediator.ChangeMusicVolume;
                    break;
                case GameVolumeSlider gameVolumeSlider:
                    gameVolumeSlider.SliderChangedValueEvent +=
                        servicesMediator.ChangeGameVolume;
                    break;
                case UiVolumeSlider uiVolumeSlider:
                    uiVolumeSlider.SliderChangedValueEvent +=
                        servicesMediator.ChangeUiVolume;
                    break;
            }
        }

        public void RemoveSlider(UiSlider uiSlider)
        {
            switch (uiSlider)
            {
                case MusicVolumeSlider musicVolumeSlider:
                    musicVolumeSlider.SliderChangedValueEvent -=
                        servicesMediator.ChangeMusicVolume;
                    break;
                case GameVolumeSlider gameVolumeSlider:
                    gameVolumeSlider.SliderChangedValueEvent -=
                        servicesMediator.ChangeGameVolume;
                    break;
                case UiVolumeSlider uiVolumeSlider:
                    uiVolumeSlider.SliderChangedValueEvent -=
                        servicesMediator.ChangeUiVolume;
                    break;
            }
        }

        public void AddToggle(UiToggle uiToggle)
        {
            
        }

        public void RemoveToggle(UiToggle uiToggle)
        {
            
        }

        public void AddDropdown(UiDropdown uiDropdown)
        {
            switch (uiDropdown)
            {
                case ResolutionDropdown resolutionDropdown:
                    resolutionDropdown.DropdownChangedValueEvent +=
                        servicesMediator.ChangeResolution;
                    break;
                case QualityDropdown qualityDropdown:
                    qualityDropdown.DropdownChangedValueEvent +=
                        servicesMediator.ChangeQuality;
                    break;
                case AliasingDropdown aliasingDropdown:
                    aliasingDropdown.DropdownChangedValueEvent +=
                        servicesMediator.ChangeAliasing;
                    break;
                case FullscreenModeDropdown fullscreenModeDropdown:
                    fullscreenModeDropdown.DropdownChangedValueEvent +=
                        servicesMediator.ChangeFullscreenMode;
                    break;
                case LanguageDropdown languageDropdown:
                    languageDropdown.DropdownChangedValueEvent +=
                        servicesMediator.ChangeLocalization;
                    break;
                case PlayersCountDropdown playersCountDropdown:
                    playersCountDropdown.DropdownChangedValueEvent +=
                        SetPlayersCountForRoom;
                    SetPlayersCountForRoom(playersCountDropdown.GetValue());
                    break;
            }
        }

        public void RemoveDropdown(UiDropdown uiDropdown)
        {
            switch (uiDropdown)
            {
                case ResolutionDropdown resolutionDropdown:
                    resolutionDropdown.DropdownChangedValueEvent -=
                        servicesMediator.ChangeResolution;
                    break;
                case QualityDropdown qualityDropdown:
                    qualityDropdown.DropdownChangedValueEvent -=
                        servicesMediator.ChangeQuality;
                    break;
                case AliasingDropdown aliasingDropdown:
                    aliasingDropdown.DropdownChangedValueEvent -=
                        servicesMediator.ChangeAliasing;
                    break;
                case FullscreenModeDropdown fullscreenModeDropdown:
                    fullscreenModeDropdown.DropdownChangedValueEvent -=
                        servicesMediator.ChangeFullscreenMode;
                    break;
                case LanguageDropdown languageDropdown:
                    languageDropdown.DropdownChangedValueEvent -=
                        servicesMediator.ChangeLocalization;
                    break;
                case PlayersCountDropdown playersCountDropdown:
                    playersCountDropdown.DropdownChangedValueEvent -=
                        SetPlayersCountForRoom;
                    break;
            }
        }

        private void SetPlayersCountForRoom(int dropdownId)
        {
            servicesMediator.SetWantedRoomPlayersCount(dropdownId + 2);
        }
    }
}