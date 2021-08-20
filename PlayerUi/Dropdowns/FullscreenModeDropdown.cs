namespace Master.QSpaceCode.PlayerUi.Dropdowns
{
    public class FullscreenModeDropdown : UiDropdown
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            LoadDropdownData(Core.SettingsKeeper.GetFullscreenVariants());
            dropdown.SetValueWithoutNotify(Core.SettingsKeeper.GetFullscreenId());
        }
    }
}