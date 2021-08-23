namespace Master.QSpaceCode.Services.Mediator
{
    public sealed partial class ServicesMediator
    {
        public void CloseCurrentUiArea()
        {
            uiService.CloseCurrentUiArea();
        }

        public void OpenMainMenuSingleplayer()
        {
            uiService.OpenMainMenuSingleplayer();
        }

        public void OpenMainMenuMultiplayer()
        {
            uiService.OpenMainMenuMultiplayer();
        }

        public void OpenMainMenuRoomSettings()
        {
            uiService.OpenMainMenuRoomSettings();
        }

        public void CloseMainMenuRoomSettings()
        {
            uiService.CloseMainMenuRoomSettings();
        }

        public void OpenMainMenuShipEditor()
        {
            uiService.OpenMainMenuShipEditor();
        }

        public void OpenMainMenuGameSettings()
        {
            uiService.OpenMainMenuGameSettings();
        }

        public void OpenMainMenuGraphicSettings()
        {
            uiService.OpenMainMenuGraphicSettings();
        }

        public void OpenMainMenuExit()
        {
            uiService.OpenMainMenuExit();
        }

        public void OpenDisconnectWindow()
        {
            uiService.OpenGameDisconnect();
        }

        public void OpenPauseWindow()
        {
            uiService.OpenGamePause();
        }
    }
}