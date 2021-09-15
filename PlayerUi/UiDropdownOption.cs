using Master.QSpaceCode.Configs;
using Master.QSpaceCode.Static;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Master.QSpaceCode.PlayerUi
{
    public class UiDropdownOption : MonoBehaviour, IPointerEnterHandler, ISelectHandler
    {
        private Toggle toggle;

        private void Awake()
        {
            toggle = GetComponent<Toggle>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            toggle.Select();
        }

        public void OnSelect(BaseEventData eventData)
        {
            if (UiSelectingController.LastSelectedGameObject != gameObject)
            {
                Core.SoundsKeeper.PlayUiSound(CurrentConfigs.AudioConfig.ButtonSelected);
            }
        }
    }
}