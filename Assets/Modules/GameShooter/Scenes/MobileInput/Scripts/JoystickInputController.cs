using System;
using StansAssets.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

namespace Game.GamePlay
{
    public class JoystickInputController : MonoBehaviour, IInputService, ISceneManager
    {
        public event Action OnFire;
        public float Horizontal => m_Joystick.Horizontal;
        public float Vertical => m_Joystick.Vertical;

        [SerializeField]
        Joystick m_Joystick = default;

        [SerializeField]
        Button m_FireButton = default;

        void Awake()
        {
            m_FireButton.onClick.AddListener(() =>
            {
                OnFire?.Invoke();
            });
        }
    }
}
