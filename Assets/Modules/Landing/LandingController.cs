using StansAssets.ProjectSample.Core;
using UnityEngine;

namespace StansAssets.ProjectSample.Landing
{
    class LandingController : MonoBehaviour
    {
        void Start()
        {
            App.Init(() =>
            {
                App.State.Set(AppState.Menu);
                
                var sceneService = App.Services.Get<ISceneService>();
                sceneService.Unload(AppConfig.LandingSceneName, null);
            });
        }
    }
}