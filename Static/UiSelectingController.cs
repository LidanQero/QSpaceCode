using UnityEngine;
using UnityEngine.EventSystems;

namespace Master.QSpaceCode.Static
{
    public static class UiSelectingController
    {
        public static GameObject LastSelectedGameObject { get; private set; }

        public static void UpdateHelper()
        {
            var eventSystem = EventSystem.current;
            if (!eventSystem) return;

            if (eventSystem.currentSelectedGameObject != null)
            {
                LastSelectedGameObject = eventSystem.currentSelectedGameObject;
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(LastSelectedGameObject);
            }
        }
    }
}