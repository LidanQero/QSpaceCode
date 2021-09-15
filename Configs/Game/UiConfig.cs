using Master.QSpaceCode.PlayerUi;
using Master.QSpaceCode.PlayerUi.Buttons;
using UnityEngine;

namespace Master.QSpaceCode.Configs.Game
{
    [CreateAssetMenu(fileName = "UIConfig", menuName = "Game Configs/UI Config", order = 0)]
    public sealed class UiConfig : ScriptableObject
    {
        [SerializeField] private RoomButton roomButton;
        [SerializeField] private PlayerImage playerImage;

        public RoomButton RoomButton => roomButton;
        public PlayerImage PlayerImage => playerImage;
    }
}