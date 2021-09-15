using System;
using Master.QSpaceCode.Configs;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Master.QSpaceCode.PlayerUi
{
    public abstract class UiSlider : MonoBehaviour, IPointerEnterHandler, ISelectHandler
    {
        [SerializeField] private bool selectWhenEnterCursor = true;

        public event Action<float> SliderChangedValueEvent;

        protected Slider slider;
        
        protected virtual void Awake()
        {
            slider = GetComponent<Slider>();
            slider.onValueChanged.AddListener(delegate(float value)
            {
                Core.SoundsKeeper.PlayUiSound(CurrentConfigs.AudioConfig.SliderChanged);
                SliderChangedValueEvent?.Invoke(value);
            });
        }

        protected virtual void OnEnable()
        {
            Core.UiInputKeeper.AddSlider(this);
        }

        protected virtual void OnDisable()
        {
            Core.UiInputKeeper.RemoveSlider(this);
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!selectWhenEnterCursor) return;
            if (slider.navigation.mode == Navigation.Mode.None) return;
            if (!slider.interactable) return;
            slider.Select();
        }

        public void OnSelect(BaseEventData eventData)
        {
            if (Core.UIHelper.LastSelectedGameObject != gameObject)
            {
                Core.SoundsKeeper.PlayUiSound(CurrentConfigs.AudioConfig.ButtonSelected);
            }
        }
    }
}