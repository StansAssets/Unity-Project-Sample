using System;
using StansAssets.Foundation.Patterns;
using StansAssets.SceneManagement;

namespace StansAssets.ProjectSample.Core
{
    public static class App
    {
        static readonly ServiceLocator s_Services = new ServiceLocator();
        static readonly ApplicationStateStack<AppState> s_State = new ApplicationStateStack<AppState>();
        
        public static IReadOnlyServiceLocator Services => s_Services;
        public static IReadOnlyApplicationStateStack<AppState> State => s_State;

        internal static void Init(Action onComplete)
        {
            var sceneService = new DefaultSceneLoadService();
            s_Services.Register<ISceneService>(sceneService);
            s_Services.Register<IPoolingService>(new GameObjectsPool("GameObjects Pool"));
            
            sceneService.PreparePreloader(() =>
            {
                InitSatesStack();
                var unused = new PreloadManager(s_State, sceneService.Preloader);
                onComplete.Invoke();
            });
        }

        static void InitSatesStack()
        {
            s_State.RegisterState(AppState.Menu, new MenuAppState());
            s_State.RegisterState(AppState.Game, new GameAppState());
            s_State.RegisterState(AppState.Pause, new PauseAppState());
        }
    }
}
