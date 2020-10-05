using System;
using StansAssets.SceneManagement;
using UnityEngine.Assertions;

namespace StansAssets.ProjectSample.Core
{
    class DefaultSceneLoadService : SceneLoadService, ISceneService
    {
        public IScenePreloader Preloader { get; private set; }

        public void PreparePreloader(Action onInit)
        {
            Load(AppConfig.MobilePreloaderSceneName, sceneManager =>
            {
                Preloader = (IScenePreloader)sceneManager;
                Assert.IsNotNull(Preloader);

                onInit.Invoke();
            });
        }
    }
}
