using Master.QSpaceCode.Services.Mediator;
using Master.QSpaceCode.Services.ServicesInterfaces;
using UnityEngine.SceneManagement;

namespace Master.QSpaceCode.Services.ServicesClasses
{
    public sealed class ScenesService : Service, IScenesService
    {
        public ScenesService(ServicesMediator newServicesMediator) : base(newServicesMediator)
        {
            SceneManager.sceneLoaded += SceneLoaded;
        }

        private void SceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
        {
            if (scene.name.Equals(Core.ScenesConfig.MenuSceneName))
                servicesMediator.FinishLoadingMenuScene();
        }
    }
}