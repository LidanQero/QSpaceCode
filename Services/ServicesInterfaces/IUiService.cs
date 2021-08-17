using System;

namespace Master.QSpaceCode.Services.ServicesInterfaces
{
    public interface IUiService
    {
        void CloseCurrentWindow();
        void UpdatePunState(PunState punState);
        void OpenMainMenu();
        void OpenSingleplayer();
        void OpenMultiplayer();
        void OpenRoomSettings();
        void CloseRoomSettings();
        void OpenShipEditor();
    }
}