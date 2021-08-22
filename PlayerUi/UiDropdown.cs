using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Master.QSpaceCode.PlayerUi
{
    [RequireComponent(typeof(TMP_Dropdown))]
    public abstract class UiDropdown : MonoBehaviour, IPointerEnterHandler, ISelectHandler
    {
        [SerializeField] private bool selectWhenEnterCursor = true;

        public event Action<int> DropdownChangedValueEvent;

        protected TMP_Dropdown dropdown;

        protected virtual void Awake()
        {
            dropdown = GetComponent<TMP_Dropdown>();
            dropdown.onValueChanged.AddListener(delegate(int value)
            {
                Core.SoundsKeeper.PlayUiSound(Core.AudioConfig.DropdownChanged);
                DropdownChangedValueEvent?.Invoke(value);
            });
        }

        protected virtual void OnEnable()
        {
            Core.UiInputKeeper.AddDropdown(this);
        }

        protected virtual void OnDisable()
        {
            Core.UiInputKeeper.RemoveDropdown(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!selectWhenEnterCursor) return;
            if (dropdown.navigation.mode == Navigation.Mode.None) return;
            if (!dropdown.interactable) return;
            if (EventSystem.current && EventSystem.current.currentSelectedGameObject &&
                EventSystem.current.currentSelectedGameObject.GetComponent<UiDropdownOption>())
                return;
            dropdown.Select();
        }

        public void OnSelect(BaseEventData eventData)
        {
            if (Core.UIHelper.LastSelectedGameObject != gameObject)
            {
                Core.SoundsKeeper.PlayUiSound(Core.AudioConfig.ButtonSelected);
            }
        }

        protected void LoadDropdownData(string[] values)
        {
            dropdown.ClearOptions();

            var optionDataList =
                values.Select(value => new TMP_Dropdown.OptionData {text = value}).ToList();

            dropdown.AddOptions(optionDataList);
        }

        public int GetValue()
        {
            return dropdown.value;
        }
    }
}