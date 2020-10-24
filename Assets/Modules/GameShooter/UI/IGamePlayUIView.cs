using System;
using StansAssets.SceneManagement;

namespace Modules.Game.UI
{
    public interface IGamePlayUIView : ISceneManager
    {
        void SetScore(int score);
        void SetLivesCount(int total, int count);

        void ShowGameOverScreen();
        void ShowGamePlayUI();

        event Action OnRestartRequest;
        event Action OnPauseRequest;
    }
}
