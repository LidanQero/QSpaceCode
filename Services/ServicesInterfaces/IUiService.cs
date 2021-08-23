using System;

namespace Master.QSpaceCode.Services.ServicesInterfaces
{
    public interface IUiService
    {
        void CloseCurrentUiArea();
        void UpdatePunState(PunState punState);
        void OpenMainMenuTitle();
        void OpenGameMain();
        void OpenGamePause();
        void OpenGameDisconnect();
        void OpenMainMenuSingleplayer();
        void OpenMainMenuMultiplayer();
        void OpenMainMenuRoomSettings();
        void CloseMainMenuRoomSettings();
        void OpenMainMenuShipEditor();
        void OpenMainMenuGameSettings();
        void OpenMainMenuGraphicSettings();
        void OpenMainMenuExit();
    }
}