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
            if (Core.UIHelper.LastSelectedGameObject != gameObject)
            {
                Core.SoundsKeeper.PlayUiSound(Core.AudioConfig.ButtonSelected);
            }
        }
    }
}