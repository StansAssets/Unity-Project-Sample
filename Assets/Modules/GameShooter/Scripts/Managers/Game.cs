using System;
using Modules.Game.UI;
using StansAssets.Foundation.Patterns;
using StansAssets.ProjectSample.Core;
using UnityEngine;

namespace Game.GamePlay
{
    [RequireComponent(typeof(GameEditMode))]
    public class Game : MonoBehaviour
    {
        [SerializeField]
        Camera m_Camera = default;
        
        public static IReadOnlyServiceLocator Services => s_Services;
        static readonly ServiceLocator s_Services = new ServiceLocator();
        
        void Awake()
        {
            s_Services.Register(new CameraService(m_Camera));
            
            var sceneService = App.Services.Get<ISceneService>();
            sceneService.Load("ShooterGameUIScene", manager =>
            {
                s_Services.Register(new AsteroidsService((IGamePlayUIView) manager));
            });
        }

        void OnDestroy()
        {
            s_Services.Clear();
            var sceneService = App.Services.Get<ISceneService>();
            sceneService.Unload("ShooterGameUIScene", () => { });
        }
    }
}