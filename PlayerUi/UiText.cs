using TMPro;
using UnityEngine;

namespace Master.QSpaceCode.PlayerUi
{
    [RequireComponent(typeof(TMP_Text))]
    public abstract class UiText : MonoBehaviour
    {
        protected TMP_Text tmpText;

        private void Awake()
        {
            tmpText = GetComponent<TMP_Text>();
        }
    }
}