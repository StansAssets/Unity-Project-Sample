using System;
using StansAssets.SceneManagement;
using UnityEngine;

namespace StansAssets.ProjectSample.Core
{
    public class GameAppState : ApplicationState
    {
        protected override void OnChangeState(StackChangeEvent<AppState> evt, IProgressReporter reporter)
        {
            switch (evt.Action)
            {
                case StackAction.Added:
                    /*
                    AddSceneAction(SceneActionType.Load, GameConfig.GamePlaySceneName);
                    AddSceneAction(SceneActionType.Load, GameConfig.InGameBackUISceneName);
                    */
                    
                    AddSceneAction(SceneActionType.Load, AppConfig.ShooterGameScene);
                    break;
                case StackAction.Removed:
                    /*
                    AddSceneAction(SceneActionType.Unload, GameConfig.GamePlaySceneName);
                    AddSceneAction(SceneActionType.Unload, GameConfig.InGameBackUISceneName);
                    */
                    
                    AddSceneAction(SceneActionType.Unload, AppConfig.ShooterGameScene);
                    break;
                case StackAction.Paused:
                    Time.timeScale = 0f;
                    break;
                case StackAction.Resumed:
                    Time.timeScale = 1f;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(evt.Action), evt.Action, null);
            }
        }
    }
}
