using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Master.QSpaceCode.PlayerUi
{
    public abstract class UiButton : MonoBehaviour, IPointerEnterHandler, ISelectHandler
    {
        [SerializeField] private bool selectWhenEnterCursor = true;

        public event Action ButtonUsingEvent;
        
        private Button button;

        private void Awake()
        {
            button = GetComponent<Button>();
            button.onClick.AddListener(delegate
            {
                Core.SoundsKeeper.PlayUiSound(Core.AudioConfig.ButtonPressed);
                ButtonUsingEvent?.Invoke();
            });
        }

        protected virtual void OnEnable()
        {
            Core.UiInputKeeper.AddButton(this);
        }

        protected virtual void OnDisable()
        {
            Core.UiInputKeeper.RemoveButton(this);
        }

        protected void PressButton()
        {
            button.OnSubmit(null);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!selectWhenEnterCursor) return;
            if (button.navigation.mode == Navigation.Mode.None) return;
            if (!button.interactable) return;
            button.Select();
        }

        public void OnSelect(BaseEventData eventData)
        {
            if (Core.UIHelper.LastSelectedGameObject != gameObject)
            {
                Core.SoundsKeeper.PlayUiSound(Core.AudioConfig.ButtonSelected);
            }
        }

        public void SetButtonInteractable(bool value)
        {
            button.interactable = value;
        }
    }
}