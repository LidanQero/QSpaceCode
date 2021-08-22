namespace Master.QSpaceCode.PlayerUi.Buttons
{
    public class BackButton : UiButton
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