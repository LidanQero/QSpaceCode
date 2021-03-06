using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Master.QSpaceCode.PlayerUi.MainMenu
{
    public class MainMenuGraphicSettings : SingleWindow
    {
        [SerializeField] private Selectable firsSelectable;

        public override void Open()
        {
            base.Open();
            EventSystem.current.SetSelectedGameObject(null);
            firsSelectable.Select();
        }
    }
}