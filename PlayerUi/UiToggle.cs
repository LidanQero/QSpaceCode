using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Master.QSpaceCode.PlayerUi
{
    public abstract class UiToggle : MonoBehaviour, IPointerEnterHandler, ISelectHandler
    {
        [SerializeField] private bool selectWhenEnterCursor = true;

        public event Action<bool> ToggleChangedEvent;

        protected Toggle toggle;

        protected virtual void Awake()
        {
            toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(delegate(bool value)
            {
                Core.SoundsKeeper.PlayUiSound(Core.AudioConfig.ToggleChanged);
                ToggleChangedEvent?.Invoke(value);
            });
        }
        
        protected virtual void OnEnable()
        {
            Core.UiInputKeeper.AddToggle(this);
        }

        protected virtual void OnDisable()
        {
            Core.UiInputKeeper.AddToggle(this);
        }
        

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!selectWhenEnterCursor) return;
            if (toggle.navigation.mode == Navigation.Mode.None) return;
            if (!toggle.interactable) return;
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