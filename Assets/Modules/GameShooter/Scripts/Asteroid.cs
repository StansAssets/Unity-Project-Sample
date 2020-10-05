using System;
using StansAssets.ProjectSample.Core;
using UnityEngine;

namespace Game.GamePlay
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Collider))]
    class Asteroid : PoolableGameObject
    {
        public event Action OnDestroy;
        public event Action<Collision> OnHit;
        
        [SerializeField]
        PoolableGameObject m_Explosion = default;

        void Awake()
        {
            gameObject.layer = LayerMask.NameToLayer(GameLayers.Asteroids);
        }

        void OnCollisionEnter(Collision collision)
        {
            OnHit?.Invoke(collision);
          
            
            if (!collision.gameObject.tag.Equals(GameTags.Projectile))
            {
                Explode();
            }
            
            OnDestroyAsteroid();
        }

        public void Explode()
        {
            App.Services.Get<IPoolingService>().Instantiate(m_Explosion.gameObject, transform.position, Quaternion.identity);
            OnDestroyAsteroid();
        }

        void OnDestroyAsteroid()
        {
            CancelInvoke(nameof(Explode));
            OnDestroy?.Invoke();
            OnDestroy = null;
        }

        public override void Init(Action onRelease)
        {
            Game.Services.Get<AsteroidsService>().Register(this);
            Invoke(nameof(Explode), 30f);
            OnDestroy += onRelease;
        }

        public override void Release()
        {
            Game.Services.Get<AsteroidsService>().Unregister(this);
        }
    }
}
