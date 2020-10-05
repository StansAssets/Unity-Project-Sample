using System;
using StansAssets.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Modules.Game.UI
{
    public class GamePlayUIView : MonoBehaviour, IGamePlayUIView, ISceneManager
    {
        [Header("Panels")]
        [SerializeField]
        GameObject m_GamePlayPanel = default;

        [SerializeField]
        GameObject m_GameOverPanel = default;
        
        [Header("Game Play UI")]
        [SerializeField]
        Text m_ScoresTextField = default;

        [SerializeField]
        Text m_LivesTextField = default;

        [SerializeField]
        Button m_PauseButton = default;
        
        
        [Header("Game Over UI")]
        [SerializeField]
        Button m_RestartButton = default;
        
        public event Action OnRestartRequest;
        public event Action OnPauseRequest;

        void Awake()
        {
            m_RestartButton.onClick.AddListener(() =>
            {
                OnRestartRequest?.Invoke();
            });
            
            m_PauseButton.onClick.AddListener(() =>
            {
                OnPauseRequest?.Invoke();
            });
        }

        public void SetScore(int score)
        {
            m_ScoresTextField.text = $"Score: {score}";
        }

        public void SetLivesCount(int total, int count)
        {
            m_LivesTextField.text = $"Life: {count} / {total}";
        }
        
        public void ShowGamePlayUI()
        {
            m_GamePlayPanel.SetActive(true);
            m_GameOverPanel.SetActive(false);
        }

        public void ShowGameOverScreen()
        {
            m_GamePlayPanel.SetActive(false);
            m_GameOverPanel.SetActive(true);
        }
    }
}
