using Master.QSpaceCode.Configs;
using UnityEngine;

namespace Master.QSpaceCode.PlayerUi.Game
{
    public sealed class GameUi : MonoBehaviour
    {
        [SerializeField] private SingleWindow gameMain;
        [SerializeField] private SingleWindow gamePause;
        [SerializeField] private SingleWindow gameDisconnect;
        [SerializeField] private SingleWindow mainMenuGameSettings;
        [SerializeField] private SingleWindow mainMenuGraphicSettings;
        [SerializeField] private SingleWindow mainMenuExit;

        private void OnEnable()
        {
            Core.UiStateKeeper.OnChangeGameMenuState += UpdateGameMenuState;
            UpdateGameMenuState(Core.UiStateKeeper.GetGameMenuState());
        }

        private void OnDisable()
        {
            Core.UiStateKeeper.OnChangeGameMenuState -= UpdateGameMenuState;
        }

        private void UpdateGameMenuState(GameMenuState gameMenuState)
        {
            CloseAllWindows();

            switch (gameMenuState)
            {
                case GameMenuState.Main:
                    gameMain.Open();
                    break;
                case GameMenuState.Pause:
                    gamePause.Open();
                    break;
                case GameMenuState.Disconnect:
                    gameDisconnect.Open();
                    break;
                case GameMenuState.GameSettings:
                    mainMenuGameSettings.Open();
                    break;
                case GameMenuState.GraphicSettings:
                    mainMenuGraphicSettings.Open();
                    break;
                case GameMenuState.Exit:
                    mainMenuExit.Open();
                    break;
            }
            
            Core.SoundsKeeper.PlayUiSound(CurrentConfigs.AudioConfig.WindowOpened);
        }

        private void CloseAllWindows()
        {
            gameMain.Close();
            gamePause.Close();
            gameDisconnect.Close();
            mainMenuGameSettings.Close();
            mainMenuGraphicSettings.Close();
            mainMenuExit.Close();
        }
    }
}