namespace Master.QSpaceCode.PlayerUi.Texts
{
    public class PlayerLoginText : UiText
    {
        private void OnEnable()
        {
            Core.GameInfoKeeper.OnUpdateLogin += RefreshLogin;
            RefreshLogin(Core.GameInfoKeeper.CurrentLogin);
        }

        private void OnDisable()
        {
            Core.GameInfoKeeper.OnUpdateLogin -= RefreshLogin;
        }

        private void RefreshLogin(string newLogin)
        {
            tmpText.text = newLogin;
        }
    }
}