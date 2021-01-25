using System;
using JetBrains.Annotations;
using StansAssets.ProjectSample.Boxes.GameUI;
using StansAssets.ProjectSample.Boxes.PauseUI;
using StansAssets.ProjectSample.Core;
using StansAssets.SceneManagement;

namespace StansAssets.ProjectSample.Boxes
{
    [UsedImplicitly]
    public class BoxesGamePlayAppState : ApplicationState, IAppState
    {
        const string k_GamePlaySceneName = "BoxesGamePlayLevel";
        const string k_InGameUISceneName = "BoxesInGameUI";
        const string k_PauseUISceneName = "BoxesPauseUI";

        readonly ISceneService m_SceneService;

        public AppState StateId => AppState.Game;
        BoxesGame m_BoxesGame;

        public BoxesGamePlayAppState()
        {
            m_SceneService = App.Services.Get<ISceneService>();
        }

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

                        var inGameUI = m_SceneActionsQueue.GetLoadedSceneManager<IBoxesInGameUI>();
                        inGameUI.OnPauseRequest += ShowPause;
                    });
                    break;
                case StackAction.Removed:
                    m_BoxesGame.Destroy();
                    m_SceneActionsQueue.AddAction(SceneActionType.Unload, k_GamePlaySceneName);
                    m_SceneActionsQueue.AddAction(SceneActionType.Unload, k_InGameUISceneName);
                    m_SceneActionsQueue.Start(progressReporter.UpdateProgress, progressReporter.SetDone);
                    break;
                case StackAction.Paused:
                    m_BoxesGame.Pause(true);
                    break;
                case StackAction.Resumed:
                    m_BoxesGame.Pause(false);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(evt.Action), evt.Action, null);
            }
        }

        void ShowPause()
        {
            m_BoxesGame.Pause(true);
            m_SceneService.Load<IBoxesPauseUI>(k_PauseUISceneName, (scene, manager) =>
            {
                manager.OnBack += () =>
                {
                    m_SceneService.Unload(k_PauseUISceneName, () => { });
                    m_BoxesGame.Pause(false);
                };

                manager.OnMainMenu += () =>
                {
                    m_SceneService.Unload(k_PauseUISceneName, () => { });
                    App.State.Set(AppState.MainMenu);
                };

                manager.OnRestart += () =>
                {
                    m_SceneService.Unload(k_PauseUISceneName, () => { });
                    m_BoxesGame.Restart();
                };
            });
        }
    }
}
