using System.Collections.Generic;
using ExitGames.Client.Photon;
using Master.QSpaceCode.Configs;
using Master.QSpaceCode.Services.Mediator;
using Master.QSpaceCode.Services.ServicesInterfaces;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using WebSocketSharp;

namespace Master.QSpaceCode.Services.ServicesClasses
{
    public sealed class ScenesService : Service, IScenesService, IInRoomCallbacks
    {
        public ScenesService(ServicesMediator newServicesMediator) : base(newServicesMediator)
        {
            SceneManager.sceneLoaded += SceneLoaded;
        }

        private string targetScene = "";

        public override void InitOnAwake()
        {
            base.InitOnAwake();
            PhotonNetwork.AddCallbackTarget(this);
            PhotonNetwork.AutomaticallySyncScene = true;
        }

        public void LoadGameScene()
        {
            targetScene = CurrentConfigs.ScenesConfig.GameSceneName;
            if (IsOnline() && PhotonNetwork.IsMasterClient)
                PhotonNetwork.LoadLevel(CurrentConfigs.ScenesConfig.LoadingSceneName);
            else if (!IsOnline())
                SceneManager.LoadSceneAsync(CurrentConfigs.ScenesConfig.LoadingSceneName);
        }

        public void LoadMenuScene()
        {
            targetScene = CurrentConfigs.ScenesConfig.MenuSceneName;
            if (IsOnline() && PhotonNetwork.IsMasterClient)
                PhotonNetwork.LoadLevel(CurrentConfigs.ScenesConfig.LoadingSceneName);
            else if (!IsOnline())
                SceneManager.LoadSceneAsync(CurrentConfigs.ScenesConfig.LoadingSceneName);
        }

        private void TestLoadingScenesForPun(Player targetPlayer, Hashtable changedProps)
        {
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
                if (!loadedInfos[i].Equals(CurrentConfigs.ScenesConfig.MenuSceneName)) break;
                if (i == loadedInfos.Count - 1) PlayersCompleteLoadMenuScene();
            }
            
            for (int i = 0; i < loadedInfos.Count; i++)
            {
                if (!loadedInfos[i].Equals(CurrentConfigs.ScenesConfig.LoadingSceneName)) break;
                if (i == loadedInfos.Count - 1) PlayersCompleteLoadLoadingScene();
            }
            
            for (int i = 0; i < loadedInfos.Count; i++)
            {
                if (!loadedInfos[i].Equals(CurrentConfigs.ScenesConfig.GameSceneName)) break;
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
            else if (scene.name.Equals(CurrentConfigs.ScenesConfig.LoadingSceneName))
            {
                if (targetScene.IsNullOrEmpty()) targetScene = CurrentConfigs.ScenesConfig.MenuSceneName;
                SceneManager.LoadSceneAsync(targetScene);
            }
            else if (scene.name.Equals(CurrentConfigs.ScenesConfig.GameSceneName))
            {
                servicesMediator.StartGame();
            }
            
            if (scene.name.Equals(CurrentConfigs.ScenesConfig.MenuSceneName))
            {
                servicesMediator.FinishLoadingMenuScene();
            }
            else if (scene.name.Equals(CurrentConfigs.ScenesConfig.LoadingSceneName))
            {
                servicesMediator.FinishLoadingLoadingScene();
            }
            else if (scene.name.Equals(CurrentConfigs.ScenesConfig.GameSceneName))
            {
                servicesMediator.FinishLoadingGameScene();
            }
        }

        private bool IsOnline()
        {
            return PhotonNetwork.IsConnected && PhotonNetwork.InRoom;
        }

        public void OnPlayerEnteredRoom(Player newPlayer)
        {
            
        }

        public void OnPlayerLeftRoom(Player otherPlayer)
        {
            
        }

        public void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
        {
            
        }

        public void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
        {
            TestLoadingScenesForPun(targetPlayer, changedProps);
        }

        public void OnMasterClientSwitched(Player newMasterClient)
        {
            
        }
    }
}