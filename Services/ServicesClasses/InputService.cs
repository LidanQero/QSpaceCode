using System;
using Master.QSpaceCode.PlayerUi;
using Master.QSpaceCode.PlayerUi.MainMenu.MainMenuButtons;
using Master.QSpaceCode.Services.Mediator;
using Master.QSpaceCode.Services.ServicesInterfaces;
using UnityEngine;

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

            defaultInputActions.UI.Cancel.performed += delegate { InputCancelEvent?.Invoke(); };
            
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
                    mainMenuBackButton.ButtonUsingEvent +=
                        servicesMediator.CloseCurrentUiArea;
                    break;
                case MainMenuMultiplayerButton mainMenuMultiplayerButton:
                    mainMenuMultiplayerButton.ButtonUsingEvent +=
                        servicesMediator.OpenMainMenuMultiplayer;
                    break;
                case MainMenuShipEditorButton mainMenuShipEditorButton:
                    mainMenuShipEditorButton.ButtonUsingEvent +=
                        servicesMediator.OpenMainMenuShipEditor;
                    break;
                case MainMenuSingleplayerButton mainMenuSingleplayerButton:
                    mainMenuSingleplayerButton.ButtonUsingEvent +=
                        servicesMediator.OpenMainMenuSingleplayer;
                    break;
                case MainMenuGameSettingsButton mainMenuGameSettingsButton:
                    mainMenuGameSettingsButton.ButtonUsingEvent +=
                        servicesMediator.OpenMainMenuGameSettings;
                    break;
                case MainMenuGraphicSettingsButton mainMenuGraphicSettingsButton:
                    mainMenuGraphicSettingsButton.ButtonUsingEvent +=
                        servicesMediator.OpenMainMenuGraphicSettings;
                    break;
                case MainMenuExitButton mainMenuExitButton:
                    mainMenuExitButton.ButtonUsingEvent +=
                        servicesMediator.OpenMainMenuExit;
                    break;
            }
        }

        public void RemoveButton(UiButton uiButton)
        {
            switch (uiButton)
            {
                case MainMenuBackButton mainMenuBackButton:
                    mainMenuBackButton.ButtonUsingEvent -=
                        servicesMediator.CloseCurrentUiArea;
                    break;
                case MainMenuMultiplayerButton mainMenuMultiplayerButton:
                    mainMenuMultiplayerButton.ButtonUsingEvent -=
                        servicesMediator.OpenMainMenuMultiplayer;
                    break;
                case MainMenuShipEditorButton mainMenuShipEditorButton:
                    mainMenuShipEditorButton.ButtonUsingEvent -=
                        servicesMediator.OpenMainMenuShipEditor;
                    break;
                case MainMenuSingleplayerButton mainMenuSingleplayerButton:
                    mainMenuSingleplayerButton.ButtonUsingEvent -=
                        servicesMediator.OpenMainMenuSingleplayer;
                    break;
                case MainMenuGameSettingsButton mainMenuGameSettingsButton:
                    mainMenuGameSettingsButton.ButtonUsingEvent -=
                        servicesMediator.OpenMainMenuGameSettings;
                    break;
                case MainMenuGraphicSettingsButton mainMenuGraphicSettingsButton:
                    mainMenuGraphicSettingsButton.ButtonUsingEvent -=
                        servicesMediator.OpenMainMenuGraphicSettings;
                    break;
                case MainMenuExitButton mainMenuExitButton:
                    mainMenuExitButton.ButtonUsingEvent -=
                        servicesMediator.OpenMainMenuExit;
                    break;
            }
        }
    }
}