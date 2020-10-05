using System;
using StansAssets.SceneManagement;
using UnityEngine;
using UnityEngine.Assertions;

namespace StansAssets.ProjectSample.Core
{
    [RequireComponent(typeof(GamePlayController))]
    class GamePlaySceneManager : MonoBehaviour, ISceneManager, ISceneDelegate
    {
        ISceneService m_SceneService;
        
        void Awake()
        {
            m_SceneService = App.Services.Get<ISceneService>();
        }

        void OnDestroy()
        {
            m_SceneService.Unload(AppConfig.InGameUISceneName, null);
        }

        public void ActivateScene(Action onComplete)
        {
            m_SceneService.Load(AppConfig.InGameUISceneName, manager =>
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