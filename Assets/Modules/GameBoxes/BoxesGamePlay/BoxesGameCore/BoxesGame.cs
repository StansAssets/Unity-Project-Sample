using System;
using System.Collections.Generic;
using StansAssets.Foundation.Patterns;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StansAssets.ProjectSample.Boxes
{
    class BoxesGame
    {
        readonly ServiceLocator m_GameServices = new ServiceLocator();
        readonly Scene m_GamePlayScene;
        List<IBoxesGameEntity> m_GameEntities;

        public BoxesGame(Scene gamePlayScene)
        {
            m_GamePlayScene = gamePlayScene;
        }

        public void Start(Action onStartCompleted)
        {
            InitServices();
            m_GameEntities = GetComponentsInChildren<IBoxesGameEntity>(m_GamePlayScene);
            var entitiesInitQueue = ListPool<IBoxesGameEntity>.Get();
            entitiesInitQueue.AddRange(m_GameEntities);
            InitGameEntities(entitiesInitQueue, () =>
            {
                ListPool<IBoxesGameEntity>.Release(entitiesInitQueue);
                onStartCompleted.Invoke();
            });
        }

        public void Pause(bool isPaused)
        {
            foreach (var gameEntity in m_GameEntities)
            {
                gameEntity.Pause(isPaused);
            }
        }

        public void Destroy()
        {
            foreach (var gameEntity in m_GameEntities)
            {
                gameEntity.Destroy();
            }
        }

        public void Restart()
        {
            foreach (var gameEntity in m_GameEntities)
            {
                gameEntity.Restart();
            }
        }

        void InitGameEntities(IList<IBoxesGameEntity> entities, Action onComplete)
        {
            if (entities.Count == 0)
            {
                onComplete.Invoke();
                return;
            }

            var gameEntity = entities[0];
            entities.RemoveAt(0);
            gameEntity.Init(m_GameServices, () =>
            {
                Debug.Log($"{ObjectNames.NicifyVariableName(gameEntity.GetType().Name)} Initialized!");
                InitGameEntities(entities, onComplete);
            });
        }

        void InitServices()
        {
            var servicesRoot = new GameObject("Services").transform;

            var inputGameObject = new GameObject(nameof(StandaloneInput));
            inputGameObject.transform.SetParent(servicesRoot);
            var standaloneInput = inputGameObject.AddComponent<StandaloneInput>();
            m_GameServices.Register<IInputService>(standaloneInput);
        }

        public static List<T> GetComponentsInChildren<T>(Scene scene) where T : class
        {
            var componentsList = new List<T>();
            foreach (var gameObject in scene.GetRootGameObjects())
            {
                var components = gameObject.GetComponentsInChildren<T>();
                if (components != null)
                {
                    componentsList.AddRange(components);
                }
            }

            return componentsList;
        }
    }
}
