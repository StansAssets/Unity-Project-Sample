using System;
using StansAssets.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace StansAssets.ProjectSample.Core
{
    class InGameUISceneManager : MonoBehaviour, ISceneManager, IInGameUI
    {
        [SerializeField]
        Button m_RightButton = null;

        [SerializeField]
        Button m_LeftButton = null;

        public event Action<Vector2> OnMove;

        void Awake()
        {
            m_RightButton.onClick.AddListener(() =>
            {
                OnMove?.Invoke(Vector2.right);
            });

            m_LeftButton.onClick.AddListener(() =>
            {
                OnMove?.Invoke(Vector2.left);
            });
        }
    }

}
