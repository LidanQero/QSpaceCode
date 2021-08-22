using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Master.QSpaceCode.PlayerUi.MainMenu
{
    public class MainMenuRoomSettingsWindow : SingleWindow
    {
        [SerializeField] private Selectable firstSelectable;

        public override void Open()
        {
            base.Open();
            EventSystem.current.SetSelectedGameObject(null);
            firstSelectable.Select();
        }
    }
}