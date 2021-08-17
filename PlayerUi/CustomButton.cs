using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Master.QSpaceCode.PlayerUi
{
    public class CustomButton : Selectable
    {
        [SerializeField] private Sprite selectedSprite;
        [SerializeField] private Sprite pressedSprite;
        [SerializeField] private Color pressedColor = new Color(0.75f, 0.75f, 0.75f, 1);

        private SpriteRenderer spriteRenderer;
        private Sprite defaultSprite;
        private bool interactable;

        protected override void Awake()
        {
            base.Awake();
            spriteRenderer = GetComponent<SpriteRenderer>();
            defaultSprite = spriteRenderer.sprite;
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);
            
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            
        }

        public void SetInteractable(bool isInteractable)
        {
            
        }
    }
}