using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Master.QSpaceCode.PlayerUi.Game
{
    public sealed class GameDisconnectWindow : SingleWindow
    {
        [SerializeField] private Selectable firstSelectable;

        public override void Open()
        {
            base.Open();
            EventSystem.current.SetSelectedGameObject(null);
            firstSelectable.Select();
        }
    }
}