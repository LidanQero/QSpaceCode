using UnityEngine;

namespace Master.QSpaceCode.PlayerUi.MainMenu
{
    public sealed class MainMenuUi : MonoBehaviour
    {
        [SerializeField] private MainMenuTitle mainMenuTitle;
        [SerializeField] private MainMenuSingleplayer mainMenuSingleplayer;
        [SerializeField] private MainMenuMultiplayer mainMenuMultiplayer;
        [SerializeField] private MainMenuShipEditor mainMenuShipEditor;
        [SerializeField] private MainMenuGameSettings mainMenuGameSettings;
        [SerializeField] private MainMenuGraphicSettings mainMenuGraphicSettings;
        [SerializeField] private MainMenuExit mainMenuExit;

        private void OnEnable()
        {
            Core.UiStateKeeper.ChangeMainMenuStateEvent += UpdateMainMenuState;
            UpdateMainMenuState(Core.UiStateKeeper.GetMainMenuState());
        }

        private void OnDisable()
        {
            Core.UiStateKeeper.ChangeMainMenuStateEvent -= UpdateMainMenuState;
        }

        private void UpdateMainMenuState(MainMenuState mainMenuState)
        {
            CloseAllWindows();
            
            switch (mainMenuState)
            {
                case MainMenuState.Title: mainMenuTitle.Open(); break;
                case MainMenuState.Singleplayer: mainMenuSingleplayer.Open(); break;
                case MainMenuState.Multiplayer: mainMenuMultiplayer.Open(); break;
                case MainMenuState.ShipEditor: mainMenuShipEditor.Open(); break;
                case MainMenuState.GameSettings: mainMenuGameSettings.Open(); break;
                case MainMenuState.GraphicSettings: mainMenuGraphicSettings.Open(); break;
                case MainMenuState.Exit: mainMenuExit.Open(); break;
            }
        }

        private void CloseAllWindows()
        {
            mainMenuTitle.Close();
            mainMenuSingleplayer.Close();
            mainMenuMultiplayer.Close();
            mainMenuShipEditor.Close();
            mainMenuGameSettings.Close();
            mainMenuGraphicSettings.Close();
            mainMenuExit.Close();
        }
    }
}