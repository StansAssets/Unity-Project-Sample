using System.Collections.Generic;
using Modules.Game.UI;
using StansAssets.Foundation.Patterns;
using StansAssets.ProjectSample.Core;
using UnityEngine;

namespace Game.GamePlay
{
    class AsteroidsService
    {
        const int k_TotalLives = 5;
        
        int m_Score;
        int m_Lives;
        readonly IGamePlayUIView m_GamePlayUI;
        readonly List<Asteroid> m_Asteroids = new List<Asteroid>();

        public AsteroidsService(IGamePlayUIView gamePlayUIView)
        {
            m_GamePlayUI = gamePlayUIView;
            m_GamePlayUI.OnRestartRequest += Restart;
            
            m_GamePlayUI.OnPauseRequest += () =>
            {
                App.State.Push(AppState.Pause);
            };

            Restart();
        }

        public void Register(Asteroid asteroid)
        {
            m_Asteroids.Add(asteroid);
            asteroid.OnHit += OnAsteroidHit;
        }

        public void Unregister(Asteroid asteroid)
        {
            m_Asteroids.Remove(asteroid);
            asteroid.OnHit -= OnAsteroidHit;
        }

        void Restart()
        {
            using (ListPool<Asteroid>.Get(out var asteroidsListCopy))
            {
                asteroidsListCopy.AddRange(m_Asteroids);
                foreach (var asteroid in asteroidsListCopy)
                {
                    asteroid.Explode();
                }
            }

            m_Score = 0;
            m_Lives = k_TotalLives;
            m_GamePlayUI.SetScore(m_Score);
            m_GamePlayUI.SetLivesCount(k_TotalLives, m_Lives);
            
            m_GamePlayUI.ShowGamePlayUI();
            
            Time.timeScale = 1f;
        }

        void OnAsteroidHit(Collision collision)
        {
            if (collision.gameObject.tag.Equals(GameTags.Projectile))
            {
                m_Score++;
                m_GamePlayUI.SetScore(m_Score);
            }

            if (collision.gameObject.tag.Equals(GameTags.Player))
            {
                m_Lives--;
                m_GamePlayUI.SetLivesCount(k_TotalLives, m_Lives);
                if (m_Lives <= 0)
                {
                    Time.timeScale = 0;
                    m_GamePlayUI.ShowGameOverScreen();
                }
            }
        }
    }
}
