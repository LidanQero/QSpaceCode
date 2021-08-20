namespace Master.QSpaceCode.PlayerUi.Dropdowns
{
    public class LanguageDropdown : UiDropdown
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            LoadDropdownData(Core.SettingsKeeper.GetLocalizationVariants());
            dropdown.SetValueWithoutNotify(Core.SettingsKeeper.GetLocalizationId());
        }
    }
}