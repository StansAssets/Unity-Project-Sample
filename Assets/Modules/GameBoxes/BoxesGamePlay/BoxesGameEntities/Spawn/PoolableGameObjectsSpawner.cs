using System;
using System.Collections;
using System.Collections.Generic;
using StansAssets.Foundation.Patterns;
using StansAssets.ProjectSample.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StansAssets.ProjectSample.Boxes
{
    class PoolableGameObjectsSpawner : MonoBehaviour, IBoxesGameEntity
    {
        [SerializeField, Range(1, 5)]
        float m_SpawnRang = 1f;

        [SerializeField, Range(0.1f, 5f)]
        float m_SpawnRateMin = 1f;

        [SerializeField, Range(5f, 10f)]
        float m_SpawnRateMax = 1f;

        [SerializeField]
        List<PoolableGameObject> m_ObjetsToSpawn = new List<PoolableGameObject>();

        public void Init(IReadOnlyServiceLocator services, Action onComplete)
        {
            DestroyComponent<MeshRenderer>();
            DestroyComponent<MeshFilter>();

            onComplete.Invoke();
            StartCoroutine(SpawnLoop());
        }

        public void Pause(bool isPaused)
        {

        }

        public void Restart()
        {
        }

        public void Destroy()
        {

        }

        IEnumerator SpawnLoop()
        {
            while (true)
            {
                yield return new WaitForSeconds(Random.Range(m_SpawnRateMin, m_SpawnRateMax));
                Spawn();
            }
        }

        [ContextMenu("Test Spawn")]
        void Spawn()
        {
            var index = Random.Range(0, m_ObjetsToSpawn.Count - 1);
            var spawn = App.Services.Get<IPoolingService>()
                .Instantiate<PoolableGameObject>(m_ObjetsToSpawn[index].gameObject);
        }


        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;

            var position = transform.position;
            var from = position;
            from.x -= m_SpawnRang;

            var to = position;
            to.x += m_SpawnRang;

            Gizmos.DrawLine(from, to);
        }

        void DestroyComponent<T>() where T : Component
        {
            var component = GetComponent<T>();
            if (component != null)
                Destroy(component);
        }


    }
}
