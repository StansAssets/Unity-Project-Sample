using System;
using StansAssets.SceneManagement;

namespace StansAssets.ProjectSample.Core
{
    public class GameAppState : ApplicationState
    {
        protected override void OnChangeState(StackChangeEvent<AppState> evt, IProgressReporter reporter)
        {
            switch (evt.Action)
            {
                case StackAction.Added:
                    AddSceneAction(SceneActionType.Load, GameConfig.GamePlaySceneName);
                    AddSceneAction(SceneActionType.Load, GameConfig.InGameBackUISceneName);
                    break;
                case StackAction.Removed:
                    AddSceneAction(SceneActionType.Unload, GameConfig.GamePlaySceneName);
                    AddSceneAction(SceneActionType.Unload, GameConfig.InGameBackUISceneName);
                    break;
                case StackAction.Paused:
                    break;
                case StackAction.Resumed:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(evt.Action), evt.Action, null);
            }
        }
    }
}
