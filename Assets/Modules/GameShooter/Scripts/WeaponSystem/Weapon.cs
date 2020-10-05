using StansAssets.ProjectSample.Core;
using UnityEngine;

namespace Game.GamePlay
{
    class Weapon : MonoBehaviour
    {
        /// <summary>
        /// Effects to appear at the muzzle of the gun (muzzle flash, smoke, etc.)
        /// </summary>
        [SerializeField, Header("Muzzle")]
        PoolableGameObject m_MuzzleEffects = default;

        /// <summary>
        /// The spot where the muzzle effects should appear from
        /// </summary>
        [SerializeField]
        Transform m_MuzzleEffectsPosition = default;

        /// <summary>
        /// The projectile to be launched (if the type is projectile)
        /// </summary>
        [SerializeField, Header("Projectile")]
        PoolableGameObject m_Projectile = default;

        [SerializeField]
        Transform m_ProjectileSpawnSpot = default;

        /// <summary>
        /// Sound to play when the weapon is fired
        /// </summary>
        [SerializeField, Header("Sound")]
        AudioClip m_FireSound = default;

        [SerializeField]
        float m_FireRate = 2.5f;

        float m_LastFireTime = 0f;
        
        void Update()
        {
            if (Input.GetKeyDown("space"))
            {
                Fire();
            }
        }

        void Fire()
        {
            if (!Mathf.Approximately(m_LastFireTime, 0f)  
                && m_LastFireTime + m_FireRate > Time.time)
            {
                return;
            }
            
            m_LastFireTime = Time.time;
            App.Services.Get<IPoolingService>().Instantiate(m_MuzzleEffects.gameObject, m_MuzzleEffectsPosition.position, m_MuzzleEffectsPosition.rotation);
            App.Services.Get<IPoolingService>().Instantiate(m_Projectile.gameObject, m_ProjectileSpawnSpot.position, m_ProjectileSpawnSpot.rotation);
            
            GetComponent<AudioSource>().PlayOneShot(m_FireSound);
        }
    }
}
