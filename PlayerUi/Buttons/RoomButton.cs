using System;
using TMPro;

namespace Master.QSpaceCode.PlayerUi.Buttons
{
    public class RoomButton : UiButton
    {
        public event Action<RoomButton> UseRoomButtonEvent;
        
        public string RoomName { get; private set; }

        private TMP_Text tmpText;

        protected override void Awake()
        {
            base.Awake();
            tmpText = GetComponentInChildren<TMP_Text>();
            ButtonPressedEvent += delegate { UseRoomButtonEvent?.Invoke(this); };
        }

        public void SetRoomName(string newName, int currentPlayers, int maxPlayers)
        {
            SetButtonInteractable(currentPlayers < maxPlayers);
            tmpText.text = $"{newName} ({currentPlayers}/{maxPlayers})";
            RoomName = newName;
        }
    }
}