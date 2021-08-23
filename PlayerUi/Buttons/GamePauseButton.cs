using UnityEngine.EventSystems;

namespace Master.QSpaceCode.PlayerUi.Buttons
{
    public class GamePauseButton : UiButton
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            Core.UiInputKeeper.InputPauseEvent += PressButton;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Core.UiInputKeeper.InputPauseEvent -= PressButton;
        }
        
        
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            Core.SoundsKeeper.PlayUiSound(Core.AudioConfig.ButtonSelected);
        }
    }
}