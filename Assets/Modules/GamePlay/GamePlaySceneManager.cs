using System;
using StansAssets.SceneManagement;
using UnityEngine;
using UnityEngine.Assertions;

namespace StansAssets.ProjectSample.Core
{
    [RequireComponent(typeof(GamePlayController))]
    class GamePlaySceneManager : MonoBehaviour, ISceneManager, ISceneDelegate
    {
        public void OnSceneLoaded()
        {

        }

        public void OnSceneUnload()
        {
            GameServices.SceneService.Unload(GameConfig.InGameUISceneName, null);
        }

        public void ActivateScene(Action onComplete)
        {
            GameServices.SceneService.Load(GameConfig.InGameUISceneName, manager =>
            {
                var inGameUI = (IInGameUI)manager;
                Assert.IsNotNull(inGameUI);

                GetComponent<GamePlayController>().Init(inGameUI);
                onComplete.Invoke();
            });
        }

        public void DeactivateScene(Action onComplete)
        {

        }
    }
}
