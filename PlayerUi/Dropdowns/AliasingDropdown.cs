namespace Master.QSpaceCode.PlayerUi.Dropdowns
{
    public class AliasingDropdown : UiDropdown
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            LoadDropdownData(Core.SettingsKeeper.GetAliasingVariants());
            dropdown.SetValueWithoutNotify(Core.SettingsKeeper.GetAliasingId());
        }
    }
}