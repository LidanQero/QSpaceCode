using Master.QSpaceCode.Configs;
using UnityEngine.EventSystems;

namespace Master.QSpaceCode.PlayerUi.Buttons
{
    public class GamePauseButton : UiButton
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            Core.UiInputKeeper.OnInputPause += PressButton;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Core.UiInputKeeper.OnInputPause -= PressButton;
        }
        
        
        public override void OnPointerEnter(PointerEventData eventData)
        {
            base.OnPointerEnter(eventData);
            Core.SoundsKeeper.PlayUiSound(CurrentConfigs.AudioConfig.ButtonSelected);
        }
    }
}