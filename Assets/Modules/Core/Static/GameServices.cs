using System;

namespace StansAssets.ProjectSample.Core
{
    public static class GameServices
    {
        public static ISceneService SceneService { get; private set; }

        // TODO remove in active project
        // Do not use method boyd as "good" example.
        // Place all the game init logic inside the Init method boyd,
        // and make sure to invoke onComplete when initialization is done.
        public static void Init(Action onComplete)
        {
            SceneService = new DefaultSceneLoadService(() =>
            {
                Game.Init();
                onComplete.Invoke();
            });
        }
    }
}
