using System;
using UnityEngine;

namespace Game.GamePlay
{
    [ExecuteInEditMode]
    public class GameEditMode : MonoBehaviour
    {
        void Awake()
        {
            gameObject.name = "Game Play Controller";
            if (Application.isPlaying)
                enabled = false;
        }
    }
}
