using System;

namespace Modules.Game.UI
{
    public interface IGamePlayUIView
    {
        void SetScore(int score);
        void SetLivesCount(int total, int count);

        void ShowGameOverScreen();
        void ShowGamePlayUI();

        event Action OnRestartRequest;
        event Action OnPauseRequest;
    }
}
