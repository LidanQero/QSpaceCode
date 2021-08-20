using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Master.QSpaceCode.PlayerUi.MainMenu
{
    public class MainMenuGameSettings : SingleWindow
    {
        [SerializeField] private Selectable firsSelectable;
        [SerializeField] private bool saveLastSelectable;
        private Selectable lastSelectable;

        public override void Open()
        {
            base.Open();
            if (lastSelectable)
            {
                EventSystem.current.SetSelectedGameObject(null);
                lastSelectable.Select();
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(null);
                firsSelectable.Select();
            }
        }
        
        public override void Close()
        {
            if (saveLastSelectable && gameObject.activeInHierarchy && EventSystem.current)
            {
                var selectedGo = EventSystem.current.currentSelectedGameObject;
                if (selectedGo) lastSelectable = selectedGo.GetComponent<Selectable>();
            }
            
            base.Close();
        }
    }
}