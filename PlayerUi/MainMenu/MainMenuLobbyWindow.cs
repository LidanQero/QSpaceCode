using System.Collections.Generic;
using Master.QSpaceCode.Configs;
using Master.QSpaceCode.PlayerUi.Buttons;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Master.QSpaceCode.PlayerUi.MainMenu
{
    public sealed class MainMenuLobbyWindow : SingleWindow
    {
        [SerializeField] private Selectable firsSelectable;
        [SerializeField] private Transform roomButtonsParent;

        private readonly List<RoomButton> roomButtons = new List<RoomButton>();

        public override void Open()
        {
            base.Open();
            EventSystem.current.SetSelectedGameObject(null);
            firsSelectable.Select();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Core.GameInfoKeeper.OnRoomUpdate += UpdateRoomsButtons;
            UpdateRoomsButtons(Core.GameInfoKeeper.Rooms);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Core.GameInfoKeeper.OnRoomUpdate -= UpdateRoomsButtons;
        }

        private void UpdateRoomsButtons(List<RoomInfo> rooms)
        {
            ClearRooms();
            foreach (var room in rooms)
            {
                var newButton = Instantiate(CurrentConfigs.UiConfig.RoomButton, roomButtonsParent, true);
                newButton.transform.localScale = Vector3.one;
                newButton.SetRoomName(room.Name, room.PlayerCount, room.MaxPlayers);
                roomButtons.Add(newButton);
            }
        }

        private void ClearRooms()
        {
            while (roomButtons.Count > 0)
            {
                Destroy(roomButtons[0].gameObject);
                roomButtons.RemoveAt(0);
            }
        }
    }
}