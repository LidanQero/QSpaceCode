namespace Master.QSpaceCode.PlayerUi.Dropdowns
{
    public class PlayersCountDropdown : UiDropdown
    {
        protected override void Awake()
        {
            base.Awake();
            LoadDropdownData(new []{"2", "3", "4"});
        }
    }
}