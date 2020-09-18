using StansAssets.SceneManagement;

namespace StansAssets.ProjectSample.Core
{
    public static class Game
    {
        static PreloadManager s_preloadManager;
        static readonly ApplicationStateStack<AppState> s_State = new ApplicationStateStack<AppState>();
        public static IApplicationStateStack<AppState> State => s_State;

        internal static void Init()
        {
            s_State.RegisterState(AppState.Menu, new MenuAppState());
            s_State.RegisterState(AppState.Game, new GameAppState());
            
            s_preloadManager = new PreloadManager(State, GameServices.SceneService.Preloader);
        }
    }
}
