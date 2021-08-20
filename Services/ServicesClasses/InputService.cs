using System;
using Master.QSpaceCode.PlayerUi;
using Master.QSpaceCode.PlayerUi.Buttons;
using Master.QSpaceCode.PlayerUi.Dropdowns;
using Master.QSpaceCode.PlayerUi.Sliders;
using Master.QSpaceCode.Services.Mediator;
using Master.QSpaceCode.Services.ServicesInterfaces;
using TMPro;
using UnityEngine.EventSystems;

namespace Master.QSpaceCode.Services.ServicesClasses
{
    public sealed class InputService : Service, IInputService, IUiInputKeeper
    {
        public InputService(ServicesMediator newServicesMediator) : base(newServicesMediator)
        {
        }

        public event Action<SystemInputMap> ChangeSystemInputMapEvent;
        public event Action InputCancelEvent;

        private SystemInputMap currentSystemInputMap;
        private DefaultInputActions defaultInputActions;

        public override void Init()
        {
            base.Init();

            defaultInputActions = new DefaultInputActions();
            defaultInputActions.Enable();

            defaultInputActions.UI.Cancel.performed += delegate
            {
                if (EventSystem.current && 
                    EventSystem.current.
                        currentSelectedGameObject.GetComponent<UiDropdownOption>()) return;
                InputCancelEvent?.Invoke();
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
        }

        public SystemInputMap GetSystemInputMap() => currentSystemInputMap;

        public void AddButton(UiButton uiButton)
        {
            switch (uiButton)
            {
                case MainMenuBackButton mainMenuBackButton:
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
            }
        }

        public void RemoveButton(UiButton uiButton)
        {
            switch (uiButton)
            {
                case MainMenuBackButton mainMenuBackButton:
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
            }
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
            }
        }
    }
}