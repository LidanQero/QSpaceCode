namespace Master.QSpaceCode.PlayerUi.Texts
{
    public class PlayerLoginText : UiText
    {
        private void OnEnable()
        {
            Core.PunInfoKeeper.UpdateLoginEvent += RefreshLogin;
            RefreshLogin(Core.PunInfoKeeper.GetCurrentLogin());
        }

        private void OnDisable()
        {
            Core.PunInfoKeeper.UpdateLoginEvent -= RefreshLogin;
        }

        private void RefreshLogin(string newLogin)
        {
            tmpText.text = newLogin;
        }
    }
}