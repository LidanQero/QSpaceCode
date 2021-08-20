namespace Master.QSpaceCode.PlayerUi.Dropdowns
{
    public class QualityDropdown : UiDropdown
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            LoadDropdownData(Core.SettingsKeeper.GetQualityVariants());
            dropdown.SetValueWithoutNotify(Core.SettingsKeeper.GetQualityId());
        }
    }
}