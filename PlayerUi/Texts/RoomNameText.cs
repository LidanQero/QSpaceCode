namespace Master.QSpaceCode.PlayerUi.Texts
{
    public class RoomNameText : UiText
    {
        private void OnEnable()
        {
            tmpText.text = Core.GameInfoKeeper.RoomName;
        }
    }
}