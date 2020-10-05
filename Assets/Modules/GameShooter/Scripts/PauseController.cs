using System;
using System.Collections;
using System.Collections.Generic;
using StansAssets.ProjectSample.Core;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    [SerializeField]
    Button m_ResumeButton = default;
    
    [SerializeField]
    Button m_MainMenuButton = default;

    void Awake()
    {
        m_ResumeButton.onClick.AddListener(() =>
        {
            App.State.Pop();
        });
        
        m_MainMenuButton.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            App.State.Set(AppState.Menu);
        });
    }
}
