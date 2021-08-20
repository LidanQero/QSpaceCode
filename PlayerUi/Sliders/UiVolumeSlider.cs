namespace Master.QSpaceCode.PlayerUi.Sliders
{
    public class UiVolumeSlider : UiSlider
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            slider.minValue = 0.0001f;
            slider.maxValue = 1;
            slider.SetValueWithoutNotify(Core.SettingsKeeper.GetUiVolume());
        }
    }
}