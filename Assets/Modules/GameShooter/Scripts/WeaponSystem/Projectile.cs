using System;
using StansAssets.ProjectSample.Core;
using UnityEngine;

namespace Game.GamePlay
{
    [RequireComponent(typeof(Rigidbody))]
    class Projectile : PoolableGameObject
    {
        [SerializeField]
        float m_Speed = 10.0f;

        [SerializeField]
        PoolableGameObject m_Explosion = default;
        
        Action m_OnRelease;
        Rigidbody m_Rigidbody;

        void Awake()
        {
            tag = GameTags.Projectile;
            m_Rigidbody = GetComponent<Rigidbody>();
        }

        void Update()
        {
            m_Rigidbody.velocity = transform.forward * m_Speed;
        }

        void OnCollisionEnter(Collision col)
        {
            Explode(col.contacts[0].point);
        }

        void DestroyProjectile()
        {
            m_OnRelease?.Invoke();
            m_OnRelease = null;
            CancelInvoke(nameof(DestroyProjectile));
        }

        void Explode(Vector3 position)
        {
            App.Services.Get<IPoolingService>().Instantiate(m_Explosion.gameObject, position, Quaternion.identity);
            DestroyProjectile();
        }

        public override void Init(Action onRelease)
        {
            m_Rigidbody.WakeUp();
            m_OnRelease = onRelease;
            Invoke(nameof(DestroyProjectile), 3f);
        }

        public override void Release()
        {
            m_Rigidbody.velocity = Vector3.zero;
            m_Rigidbody.Sleep();
        }
    }
}
