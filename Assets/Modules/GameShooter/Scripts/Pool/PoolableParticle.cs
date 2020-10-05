using System;
using UnityEngine;

namespace Game.GamePlay
{
    [RequireComponent(typeof(ParticleSystem))]
    class PoolableParticle : DestroyOverTimePoolable
    {
        ParticleSystem m_ParticleSystem;

        void Awake()
        {
            m_ParticleSystem = gameObject.GetComponent<ParticleSystem>();
        }

        public override void Init(Action onRelease)
        {
            m_ParticleSystem.Play();
            base.Init(onRelease);
        }
    }
}
