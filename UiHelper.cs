using UnityEngine;
using UnityEngine.EventSystems;

namespace Master.QSpaceCode
{
    public class UiHelper
    {
        private GameObject lastSelectedGameObject;

        public GameObject LastSelectedGameObject => lastSelectedGameObject;

        public void UpdateHelper()
        {
            EventSystem eventSystem = EventSystem.current;
            if (!eventSystem) return;

            if (eventSystem.currentSelectedGameObject != null)
                lastSelectedGameObject = eventSystem.currentSelectedGameObject;
            else
                EventSystem.current.SetSelectedGameObject(lastSelectedGameObject);
        }
    }
}