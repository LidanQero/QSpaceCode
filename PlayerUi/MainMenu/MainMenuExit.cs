using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Master.QSpaceCode.PlayerUi.MainMenu
{
    public class MainMenuExit : SingleWindow
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