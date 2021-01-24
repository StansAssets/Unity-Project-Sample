using System;
using StansAssets.ProjectSample.Core;
using UnityEngine;

namespace StansAssets.ProjectSample.Boxes
{
    public class EnemyBox : PoolableGameObject
    {
        public override void Init(Action onRelease)
        {
            gameObject.SetActive(true);
        }

        public override void Release()
        {

        }
    }
}
