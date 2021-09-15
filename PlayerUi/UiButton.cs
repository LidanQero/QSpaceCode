using System;
using Master.QSpaceCode.Configs;
using Master.QSpaceCode.Static;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Master.QSpaceCode.PlayerUi
{
    public abstract class UiButton : MonoBehaviour, IPointerEnterHandler, ISelectHandler
    {
        [SerializeField] private bool selectWhenEnterCursor = true;

        public event Action ButtonPressedEvent;
        
        private Button button;
        private float enabledTime;

        protected virtual void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(delegate
            {
                Core.SoundsKeeper.PlayUiSound(CurrentConfigs.AudioConfig.ButtonPressed);
                ButtonPressedEvent?.Invoke();
            });
        }

        protected virtual void OnEnable()
        {
            Core.UiInputKeeper.AddButton(this);
            enabledTime = Time.time;
        }

        protected virtual void OnDisable()
        {
            Core.UiInputKeeper.RemoveButton(this);
        }

        protected void PressButton()
        {
            if (enabledTime + 0.1f > Time.time) return;
            button.OnSubmit(null);
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            if (!selectWhenEnterCursor) return;
            if (button.navigation.mode == Navigation.Mode.None) return;
            if (!button.interactable) return;
            button.Select();
        }

        public void OnSelect(BaseEventData eventData)
        {
            if (UiSelectingController.LastSelectedGameObject != gameObject)
            {
                Core.SoundsKeeper.PlayUiSound(CurrentConfigs.AudioConfig.ButtonSelected);
            }
        }

        public void SetButtonInteractable(bool value)
        {
            button.interactable = value;
        }
    }
}