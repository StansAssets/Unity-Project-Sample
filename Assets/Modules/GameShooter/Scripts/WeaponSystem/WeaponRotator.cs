using UnityEngine;

namespace Game.GamePlay
{
    class WeaponRotator : GameEventsDelegate
    {
        [SerializeField]
        Vector2 m_RotationRange = new Vector3(70, 70);
        [SerializeField]
        float m_RotationSpeed = 10;
        [SerializeField]
        float m_DampingTime = 0.2f;

        Vector3 m_TargetAngles;
        Vector3 m_FollowAngles;
        Vector3 m_FollowVelocity;
        Quaternion m_OriginalRotation;

        IInputService m_InputService;

        void Awake()
        {
            enabled = false;
        }

        public override void OnGameInitialized()
        {
            m_OriginalRotation = transform.localRotation;
            m_InputService = Game.Services.Get<IInputService>();
            enabled = true;
        }

        // Update is called once per frame
        void Update()
        {
            var inputH = m_InputService.Horizontal;
            var inputV = m_InputService.Vertical;

            // wrap values to avoid springing quickly the wrong way from positive to negative
            if (m_TargetAngles.y > 180)
            {
                m_TargetAngles.y -= 360;
                m_FollowAngles.y -= 360;
            }

            if (m_TargetAngles.x > 180)
            {
                m_TargetAngles.x -= 360;
                m_FollowAngles.x -= 360;
            }

            if (m_TargetAngles.y < -180)
            {
                m_TargetAngles.y += 360;
                m_FollowAngles.y += 360;
            }

            if (m_TargetAngles.x < -180)
            {
                m_TargetAngles.x += 360;
                m_FollowAngles.x += 360;
            }

            m_TargetAngles.y += inputH * m_RotationSpeed;
            m_TargetAngles.x += inputV * m_RotationSpeed;

            // clamp values to allowed range
            m_TargetAngles.y = Mathf.Clamp(m_TargetAngles.y, -m_RotationRange.y * 0.5f, m_RotationRange.y * 0.5f);
            m_TargetAngles.x = Mathf.Clamp(m_TargetAngles.x, -m_RotationRange.x * 0.5f, m_RotationRange.x * 0.5f);

            // smoothly interpolate current values to target angles
            m_FollowAngles = Vector3.SmoothDamp(m_FollowAngles, m_TargetAngles, ref m_FollowVelocity, m_DampingTime);

            // update the actual gameobject rotation
            transform.localRotation = m_OriginalRotation * Quaternion.Euler(-m_FollowAngles.x, m_FollowAngles.y, 0);
        }
    }
}