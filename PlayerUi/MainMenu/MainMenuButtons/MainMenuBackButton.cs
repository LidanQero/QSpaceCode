namespace Master.QSpaceCode.PlayerUi.MainMenu.MainMenuButtons
{
    public class MainMenuBackButton : UiButton
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            Core.UiInputKeeper.InputCancelEvent += PressButton;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Core.UiInputKeeper.InputCancelEvent -= PressButton;
        }
    }
}