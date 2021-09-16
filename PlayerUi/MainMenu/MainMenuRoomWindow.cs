using System.Collections.Generic;
using Master.QSpaceCode.Configs;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Master.QSpaceCode.PlayerUi.MainMenu
{
    public class MainMenuRoomWindow : SingleWindow
    {
        [SerializeField] private Selectable firstSelectable;
        [SerializeField] private Transform playersParent;

        private readonly List<PlayerImage> playerImages = new List<PlayerImage>();

        public override void Open()
        {
            base.Open();
            EventSystem.current.SetSelectedGameObject(null);
            firstSelectable.Select();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Core.GameInfoKeeper.OnPlayersUpdate += UpdatePlayers;
            UpdatePlayers(Core.GameInfoKeeper.Players);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            Core.GameInfoKeeper.OnPlayersUpdate -= UpdatePlayers;
        }

        private void UpdatePlayers(List<Player> players)
        {
            ClearPlayers();

            foreach (var player in players)
            {
                var newImage = Instantiate(CurrentConfigs.UiConfig.PlayerImage, playersParent, true);
                newImage.transform.localScale = Vector3.one;
                newImage.SetInfo(player.NickName, player.IsMasterClient);
                playerImages.Add(newImage);
            }
        }

        private void ClearPlayers()
        {
            while (playerImages.Count > 0)
            {
                Destroy(playerImages[0].gameObject);
                playerImages.RemoveAt(0);
            }
        }
    }
}