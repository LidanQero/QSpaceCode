namespace Master.QSpaceCode.PlayerUi.Dropdowns
{
    public sealed class ResolutionDropdown : UiDropdown
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            LoadDropdownData(Core.SettingsKeeper.GetResolutionVariants());
            dropdown.SetValueWithoutNotify(Core.SettingsKeeper.GetResolutionId());
        }
    }
}