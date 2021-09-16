using System;
using TMPro;
using UnityEngine;

namespace Master.QSpaceCode.PlayerUi
{
    [RequireComponent(typeof(TMP_Text))]
    public sealed class UiLocalizedText : MonoBehaviour
    {
        [SerializeField] private string key;

        private TMP_Text tmpText;
        
        private void Awake()
        {
            tmpText = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            Core.SettingsKeeper.OnChangeLocalization += Localize;
            Localize();
        }

        private void OnDisable()
        {
            Core.SettingsKeeper.OnChangeLocalization -= Localize;
        }

        private void Localize()
        {
            tmpText.text = Core.SettingsKeeper.GetLocalizedText(key);
        }
    }
}