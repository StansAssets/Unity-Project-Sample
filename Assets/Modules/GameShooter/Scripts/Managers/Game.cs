using System;
using System.Collections.Generic;
using Modules.Game.UI;
using StansAssets.Foundation.Patterns;
using StansAssets.ProjectSample.Core;
using StansAssets.SceneManagement;
using UnityEngine;

namespace Game.GamePlay
{
    [RequireComponent(typeof(GameEditMode))]
    class Game : MonoBehaviour
    {
        [SerializeField]
        Camera m_Camera = default;

        public static IReadOnlyServiceLocator Services => s_Services;
        static readonly ServiceLocator s_Services = new ServiceLocator();
        ISceneService m_SceneService;

        [SerializeField]
        List<GameEventsDelegate> m_GameEventsDelegates = default;

        void Awake()
        {
            s_Services.Register(new CameraService(m_Camera));

            m_SceneService = App.Services.Get<ISceneService>();
            RegisterMobileInput();

            m_SceneService.Load<IGamePlayUIView>("ShooterGameUIScene", manager =>
            {
                s_Services.Register(new AsteroidsService(manager));
            });
        }

        void RegisterStandaloneInput()
        {
            var inputGameObject = new GameObject(nameof(StandaloneInput));
            inputGameObject.transform.SetParent(transform);
            var standaloneInput = inputGameObject.AddComponent<StandaloneInput>();
            s_Services.Register<IInputService>(standaloneInput);
            OnInitCompleted();
        }

        void RegisterMobileInput()
        {
            m_SceneService.Load<ISceneManager>("MobileInput", manager =>
            {
                s_Services.Register((IInputService)manager);
                OnInitCompleted();
            });
        }

        void OnInitCompleted()
        {
            foreach (var @delegate in m_GameEventsDelegates)
            {
                @delegate.OnGameInitialized();
            }
        }

        void OnDestroy()
        {
            s_Services.Clear();
            var sceneService = App.Services.Get<ISceneService>();
            sceneService.Unload("ShooterGameUIScene", () => { });
        }
    }
}
