using UnityEngine;
using UnityEngine.UI;

namespace Master.QSpaceCode.PlayerUi
{
    [RequireComponent(typeof(Image))]
    public sealed class UiKeyImage : MonoBehaviour
    {
        [SerializeField] private Sprite keyboardSprite;
        [SerializeField] private Sprite xboxSprite;
        [SerializeField] private Sprite psSprite;
        [SerializeField] private SystemInputMap systemInputMap;

        private Image image;

        private void Awake()
        {
            image = GetComponent<Image>();
        }

        private void OnEnable()
        {
            Core.UiInputKeeper.OnChangeSystemInputMap += RefreshImage;
            RefreshImage(Core.UiInputKeeper.GetSystemInputMap());
        }

        private void OnDisable()
        {
            Core.UiInputKeeper.OnChangeSystemInputMap -= RefreshImage;
        }

        private void RefreshImage(SystemInputMap newSystemInputMap)
        {
            systemInputMap = newSystemInputMap;
            switch (systemInputMap)
            {
                case SystemInputMap.Keyboard:
                    if (keyboardSprite) image.sprite = keyboardSprite;
                    break;

                case SystemInputMap.Xbox:
                    if (xboxSprite) image.sprite = xboxSprite;
                    break;

                case SystemInputMap.PS:
                    if (psSprite) image.sprite = psSprite;
                    break;
            }
        }
    }
}