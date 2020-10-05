using System;
using StansAssets.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace StansAssets.ProjectSample.Core
{
    class MainMenuController : MonoBehaviour, ISceneManager, ISceneDelegate
    {
        [SerializeField]
        Button m_PlayButton = null;

        [SerializeField]
        Button m_SettingsButton = null;

        [SerializeField]
        GameObject m_MainMenu = null;
        
        void Awake()
        {
            m_PlayButton.onClick.AddListener(() =>
            {
                App.State.Set(AppState.Game);
            });

            m_SettingsButton.onClick.AddListener(() =>
            {
                App.State.Push(AppState.Settings);
            });
        }

        public void ActivateScene(Action onComplete)
        {
            m_MainMenu.SetActive(true);
            onComplete.Invoke();
        }

        public void DeactivateScene(Action onComplete)
        {
            m_MainMenu.SetActive(false);
            onComplete.Invoke();
        }
    }
}
