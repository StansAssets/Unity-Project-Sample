using StansAssets.SceneManagement;

namespace StansAssets.ProjectSample.Core
{
    public abstract class ApplicationState : IApplicationState<AppState>
    {
        readonly SceneActionsQueue m_SceneActionsQueue;

        protected ApplicationState()
        {
            var sceneService = App.Services.Get<ISceneService>();
            m_SceneActionsQueue = new SceneActionsQueue(sceneService);
        }

        protected void AddSceneAction(SceneActionType type, string sceneName)
        {
            m_SceneActionsQueue.AddAction(type, sceneName);
        }
        
        protected abstract void OnChangeState(StackChangeEvent<AppState> evt, IProgressReporter reporter);
        public void ChangeState(StackChangeEvent<AppState> evt, IProgressReporter reporter) {
            OnChangeState(evt, reporter);
            m_SceneActionsQueue.Start(reporter.UpdateProgress, reporter.SetDone);
        }
    }
}
