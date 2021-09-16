namespace Master.QSpaceCode.PlayerUi.Buttons
{
    public class BackButton : UiButton
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            Core.UiInputKeeper.OnInputCancel += PressButton;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Core.UiInputKeeper.OnInputCancel -= PressButton;
        }
    }
}