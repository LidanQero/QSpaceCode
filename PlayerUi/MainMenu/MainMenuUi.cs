using Master.QSpaceCode.Configs;
using UnityEngine;

namespace Master.QSpaceCode.PlayerUi.MainMenu
{
    public sealed class MainMenuUi : MonoBehaviour
    {
        [SerializeField] private SingleWindow mainMenuTitle;
        [SerializeField] private SingleWindow mainMenuSingleplayer;
        [SerializeField] private SingleWindow mainMenuMultiplayer;
        [SerializeField] private SingleWindow mainMenuShipEditor;
        [SerializeField] private SingleWindow mainMenuGameSettings;
        [SerializeField] private SingleWindow mainMenuGraphicSettings;
        [SerializeField] private SingleWindow mainMenuExit;

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
                case MainMenuState.Title:
                    mainMenuTitle.Open();
                    break;
                case MainMenuState.Singleplayer:
                    mainMenuSingleplayer.Open();
                    break;
                case MainMenuState.Multiplayer:
                    mainMenuMultiplayer.Open();
                    break;
                case MainMenuState.ShipEditor:
                    mainMenuShipEditor.Open();
                    break;
                case MainMenuState.GameSettings:
                    mainMenuGameSettings.Open();
                    break;
                case MainMenuState.GraphicSettings:
                    mainMenuGraphicSettings.Open();
                    break;
                case MainMenuState.Exit:
                    mainMenuExit.Open();
                    break;
            }
            
            Core.SoundsKeeper.PlayUiSound(CurrentConfigs.AudioConfig.WindowOpened);
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