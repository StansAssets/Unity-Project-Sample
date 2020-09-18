using StansAssets.ProjectSample.Core;
using UnityEngine;

namespace StansAssets.ProjectSample
{
    class LandingController : MonoBehaviour
    {
        void Start()
        {
            GameServices.Init(() =>
            {
                Game.State.Set(AppState.Menu);
            });
        }
    }
}