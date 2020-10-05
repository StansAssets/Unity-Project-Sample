using System;
using StansAssets.SceneManagement;

namespace StansAssets.ProjectSample.Core
{
    public class MenuAppState : ApplicationState
    {
        protected override void OnChangeState(StackChangeEvent<AppState> evt, IProgressReporter reporter)
        {
            switch (evt.Action)
            {
                case StackAction.Added:
                    AddSceneAction(SceneActionType.Load, AppConfig.MainMenuSceneName);
                    break;
                case StackAction.Removed:
                    AddSceneAction(SceneActionType.Deactivate, AppConfig.MainMenuSceneName);
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
