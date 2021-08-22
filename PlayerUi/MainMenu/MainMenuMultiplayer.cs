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
            Core.UiStateKeeper.ChangeMultiplayerMenuStateEvent += UpdateMultiplayerState;
            UpdateMultiplayerState(Core.UiStateKeeper.GetMultiplayerMenuState());
        }

        public override void Close()
        {
            base.Close();
            Core.UiStateKeeper.ChangeMultiplayerMenuStateEvent -= UpdateMultiplayerState;
        }

        private void UpdateMultiplayerState(MultiplayerMenuState newMultiplayerMenuState)
        {
            CloseAllWindows();
            
            switch (newMultiplayerMenuState)
            {
                case MultiplayerMenuState.Connection: connectingWindow.Open(); break;
                case MultiplayerMenuState.Lobby: lobbyWindow.Open(); break;
                case MultiplayerMenuState.Login: loginWindow.Open(); break;
                case MultiplayerMenuState.Room: roomWindow.Open(); break;
                case MultiplayerMenuState.RoomSettings: roomSettingsWindow.Open(); break;
            }
        }

        private void CloseAllWindows()
        {
            connectingWindow.Close();
            lobbyWindow.Close();
            loginWindow.Close();
            roomSettingsWindow.Close();
            roomWindow.Close();
        }
    }
}