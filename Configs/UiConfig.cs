using Master.QSpaceCode.PlayerUi.Buttons;
using UnityEngine;

namespace Master.QSpaceCode.Configs
{
    [CreateAssetMenu(fileName = "UI Config", menuName = "GameConfigs/UI Config", order = 0)]
    public sealed class UiConfig : ScriptableObject
    {
        [SerializeField] private RoomButton roomButton;

        public RoomButton RoomButton => roomButton;
    }
}