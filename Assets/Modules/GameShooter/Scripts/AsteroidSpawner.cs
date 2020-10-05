using System.Collections;
using System.Collections.Generic;
using StansAssets.ProjectSample.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.GamePlay
{
    class AsteroidSpawner : MonoBehaviour
    {
        [SerializeField, Range(1, 5)]
        float m_SpawnRang = 1f;

        [SerializeField, Range(0.1f, 5f)]
        float m_SpawnRateMin = 1f;

        [SerializeField, Range(5f, 10f)]
        float m_SpawnRateMax = 1f;

        [SerializeField]
        List<Asteroid> m_Asteroids = new List<Asteroid>();
        
        void Awake()
        {
            DestroyComponent<MeshRenderer>();
            DestroyComponent<MeshFilter>();

            StartCoroutine(SpawnLoop());
        }

        void DestroyComponent<T>() where T : Component
        {
            var component = GetComponent<T>();
            if (component != null)
                Destroy(component);
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
            CreateAsteroid();
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

        void CreateAsteroid()
        {
            var index = Random.Range(0, m_Asteroids.Count - 1);
            var asteroid =   App.Services.Get<IPoolingService>().Instantiate<Asteroid>(m_Asteroids[index].gameObject);

            var asteroidGameObject = asteroid.gameObject;
            var spawnSpread = Random.Range(-m_SpawnRang, m_SpawnRang);
            asteroidGameObject.transform.SetParent(transform);
            asteroidGameObject.transform.localPosition = new Vector3(spawnSpread, 0, 0);
            
            var v = Game.Services.Get<CameraService>().MainCamera.transform.position - transform.position;
            asteroidGameObject.GetComponent<Rigidbody>().AddForce(v.normalized * 250f);
        }
    }
}
