using System.Collections.Generic;
using ExitGames.Client.Photon;
using Master.QSpaceCode.Services.Mediator;
using Master.QSpaceCode.Services.ServicesInterfaces;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using WebSocketSharp;

namespace Master.QSpaceCode.Services.ServicesClasses
{
    public sealed class ScenesService : Service, IScenesService
    {
        public ScenesService(ServicesMediator newServicesMediator) : base(newServicesMediator)
        {
            SceneManager.sceneLoaded += SceneLoaded;
        }

        private string targetScene = "";

        public override void Init()
        {
            base.Init();
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        public void LoadGameScene()
        {
            targetScene = Core.ScenesConfig.GameSceneName;
            if (IsOnline() && PhotonNetwork.IsMasterClient)
                PhotonNetwork.LoadLevel(Core.ScenesConfig.LoadingSceneName);
            else if (!IsOnline())
                SceneManager.LoadSceneAsync(Core.ScenesConfig.LoadingSceneName);
        }

        public void LoadMenuScene()
        {
            targetScene = Core.ScenesConfig.MenuSceneName;
            if (IsOnline() && PhotonNetwork.IsMasterClient)
                PhotonNetwork.LoadLevel(Core.ScenesConfig.LoadingSceneName);
            else if (!IsOnline())
                SceneManager.LoadSceneAsync(Core.ScenesConfig.LoadingSceneName);
        }

        public void TestLoadingScenesForPun(Player targetPlayer, Hashtable changedProps)
        {
            if (!PhotonNetwork.IsMasterClient) return;
            if (!changedProps.ContainsKey("PlayerLoadedScene")) return;
            var loadedInfos = new List<string>();

            foreach (var player in PhotonNetwork.PlayerList)
            {
                player.CustomProperties.TryGetValue("PlayerLoadedScene", out object isLoaded);
                if (isLoaded is string loadedInfo) loadedInfos.Add(loadedInfo);
                else return;
            }

            for (int i = 0; i < loadedInfos.Count; i++)
            {
                if (!loadedInfos[i].Equals(Core.ScenesConfig.MenuSceneName)) break;
                if (i == loadedInfos.Count - 1) PlayersCompleteLoadMenuScene();
            }
            
            for (int i = 0; i < loadedInfos.Count; i++)
            {
                if (!loadedInfos[i].Equals(Core.ScenesConfig.LoadingSceneName)) break;
                if (i == loadedInfos.Count - 1) PlayersCompleteLoadLoadingScene();
            }
            
            for (int i = 0; i < loadedInfos.Count; i++)
            {
                if (!loadedInfos[i].Equals(Core.ScenesConfig.GameSceneName)) break;
                if (i == loadedInfos.Count - 1) PlayersCompleteLoadGameScene();
            }
        }

        private void PlayersCompleteLoadMenuScene()
        {
            
        }
        
        private void PlayersCompleteLoadLoadingScene()
        {
            PhotonNetwork.LoadLevel(targetScene);
        }
        
        private void PlayersCompleteLoadGameScene()
        {
            servicesMediator.StartGame();
        }

        private void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (IsOnline())
            {
                var prop = new Hashtable {{"PlayerLoadedScene", scene.name}};
                PhotonNetwork.LocalPlayer.SetCustomProperties(prop);
            }
            else if (scene.name.Equals(Core.ScenesConfig.LoadingSceneName))
            {
                if (targetScene.IsNullOrEmpty()) targetScene = Core.ScenesConfig.MenuSceneName;
                SceneManager.LoadSceneAsync(targetScene);
            }
            else if (scene.name.Equals(Core.ScenesConfig.GameSceneName))
            {
                servicesMediator.StartGame();
            }
            
            if (scene.name.Equals(Core.ScenesConfig.MenuSceneName))
            {
                servicesMediator.FinishLoadingMenuScene();
            }
            else if (scene.name.Equals(Core.ScenesConfig.LoadingSceneName))
            {
                servicesMediator.FinishLoadingLoadingScene();
            }
            else if (scene.name.Equals(Core.ScenesConfig.GameSceneName))
            {
                servicesMediator.FinishLoadingGameScene();
            }
        }

        private bool IsOnline()
        {
            return PhotonNetwork.IsConnected && PhotonNetwork.InRoom;
        }
    }
}