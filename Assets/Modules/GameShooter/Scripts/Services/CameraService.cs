using UnityEngine;

namespace Game.GamePlay
{
    public class CameraService
    {
        public Camera MainCamera { get; }

        public CameraService(Camera mainCamera)
        {
            MainCamera = mainCamera;
        }
    }
}
