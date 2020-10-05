using StansAssets.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace StansAssets.ProjectSample.Core
{
    class InGameBackUISceneManager : MonoBehaviour, ISceneManager
    {
        [SerializeField]
        Button m_DoneButton = null;

        void Awake()
        {
            m_DoneButton.onClick.AddListener(() =>
            {
                App.State.Set(AppState.Menu);
            });
        }
    }
}
