using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Master.QSpaceCode.PlayerUi.MainMenu
{
    public sealed class MainMenuTitle : SingleWindow
    {
        [SerializeField] private Selectable firsSelectable;
        private Selectable lastSelectable;

        public override void Open()
        {
            base.Open();
            Invoke(nameof(SelectFirstSelectable), 0.01f);
        }

        public override void Close()
        {
            if (EventSystem.current)
            {
               var selectedGo = EventSystem.current.currentSelectedGameObject;
               if (selectedGo) lastSelectable = selectedGo.GetComponent<Selectable>();
            }
            
            base.Close();
        }

        private void SelectFirstSelectable()
        {
            if (EventSystem.current) EventSystem.current.SetSelectedGameObject(null);
            if (lastSelectable) lastSelectable.Select();
            else firsSelectable.Select();
        }
    }
}