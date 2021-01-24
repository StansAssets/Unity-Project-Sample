using System;
using JetBrains.Annotations;
using StansAssets.ProjectSample.Core;
using StansAssets.SceneManagement;

namespace StansAssets.ProjectSample.Boxes
{
    [UsedImplicitly]
    public class BoxesGamePlayAppState : ApplicationState, IAppState
    {
        const string k_GamePlaySceneName = "BoxesGamePlayLevel";
        const string k_InGameUISceneName = "BoxesInGameUI";

        public AppState StateId => AppState.Game;
        BoxesGame m_BoxesGame;

        public override void ChangeState(StackChangeEvent<AppState> evt, IProgressReporter progressReporter)
        {
            switch (evt.Action)
            {
                case StackAction.Added:
                    m_SceneActionsQueue.AddAction(SceneActionType.Load, k_GamePlaySceneName);
                    m_SceneActionsQueue.AddAction(SceneActionType.Load, k_InGameUISceneName);

                    m_SceneActionsQueue.Start(progressReporter.UpdateProgress, () =>
                    {
                        var gamePlayScene = m_SceneActionsQueue.GetLoadedScene(k_GamePlaySceneName);
                        m_BoxesGame = new BoxesGame(gamePlayScene);
                        m_BoxesGame.Start(progressReporter.SetDone);
                    });
                    break;
                case StackAction.Removed:
                    m_SceneActionsQueue.AddAction(SceneActionType.Unload, k_GamePlaySceneName);
                    m_SceneActionsQueue.AddAction(SceneActionType.Unload, k_InGameUISceneName);
                    m_SceneActionsQueue.Start(progressReporter.UpdateProgress, progressReporter.SetDone);
                    break;
                case StackAction.Paused:
                    throw new NotImplementedException();
                case StackAction.Resumed:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException(nameof(evt.Action), evt.Action, null);
            }
        }
    }
}
