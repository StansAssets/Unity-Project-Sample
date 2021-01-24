using System;
using System.Collections;
using UnityEngine;

namespace StansAssets.ProjectSample.Core
{
    public class DestroyOverTimePoolable : PoolableGameObject
    {
        [SerializeField]
        float m_PlayTime = 1f;

        Action m_OnComplete;

        public override void Init(Action onRelease)
        {
            m_OnComplete = onRelease;
            StartCoroutine(WaitForPlay());
        }

        IEnumerator WaitForPlay()
        {
            yield return new WaitForSeconds(m_PlayTime);
            m_OnComplete.Invoke();
        }

        public override void Release() { }
    }
}
