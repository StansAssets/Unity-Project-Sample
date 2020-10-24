using System;
using UnityEngine;

namespace Game.GamePlay
{
    class StandaloneInput : MonoBehaviour, IInputService
    {
        public event Action OnFire;
        
        public float Horizontal =>  Input.GetAxis("Horizontal");
        public float Vertical => Input.GetAxis("Vertical");
        
        void Update()
        {
            if (Input.GetKeyDown("space"))
            {
                OnFire?.Invoke();
            }
        }
    }
}
