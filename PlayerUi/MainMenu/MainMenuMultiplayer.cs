using UnityEngine;

namespace Master.QSpaceCode.PlayerUi.MainMenu
{
    public sealed class MainMenuMultiplayer : SingleWindow
    {
        [SerializeField] private SingleWindow connectingWindow;
        [SerializeField] private SingleWindow loginWindow;
        [SerializeField] private SingleWindow lobbyWindow;
        [SerializeField] private SingleWindow roomSettingsWindow;
        [SerializeField] private SingleWindow roomWindow;

        public override void Open()
        {
            base.Open();
            Core.UiStateKeeper.OnChangeMultiplayerMenuState += UpdateMultiplayerState;
            UpdateMultiplayerState(Core.UiStateKeeper.GetMultiplayerMenuState());
        }

        public override void Close()
        {
            base.Close();
            Core.UiStateKeeper.OnChangeMultiplayerMenuState -= UpdateMultiplayerState;
        }

        private void UpdateMultiplayerState(MultiplayerMenuState newMultiplayerMenuState)
        {
            CloseAllWindows();

            switch (newMultiplayerMenuState)
            {
                case MultiplayerMenuState.Connection:
                    if (connectingWindow) connectingWindow.Open();
                    break;
                case MultiplayerMenuState.Lobby:
                    if (lobbyWindow) lobbyWindow.Open();
                    break;
                case MultiplayerMenuState.Login:
                    if (loginWindow) loginWindow.Open();
                    break;
                case MultiplayerMenuState.Room:
                    if (roomWindow) roomWindow.Open();
                    break;
                case MultiplayerMenuState.RoomSettings:
                    if (roomSettingsWindow) roomSettingsWindow.Open();
                    break;
            }
        }

        private void CloseAllWindows()
        {
            if (connectingWindow) connectingWindow.Close();
            if (lobbyWindow) lobbyWindow.Close();
            if (loginWindow) loginWindow.Close();
            if (roomSettingsWindow) roomSettingsWindow.Close();
            if (roomWindow) roomWindow.Close();
        }
    }
}