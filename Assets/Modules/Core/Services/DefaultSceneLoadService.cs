using System;
using StansAssets.SceneManagement;
using UnityEngine.Assertions;

namespace StansAssets.ProjectSample.Core
{
    public class DefaultSceneLoadService : SceneLoadService, ISceneService
    {
        public IScenePreloader Preloader { get; private set; }

        public DefaultSceneLoadService(Action onInit)
        {
            Load(GameConfig.MobilePreloaderSceneName, sceneManager =>
            {
                Preloader = (IScenePreloader)sceneManager;
                Assert.IsNotNull(Preloader);

                onInit.Invoke();
            });
        }
    }
}
