using UnityEngine;

namespace StansAssets.ProjectSample.Core
{
    public class GamePlayController : MonoBehaviour
    {
        [SerializeField]
        GameObject m_Sphere = default;

        public void Init(IInGameUI inGameUI)
        {
            inGameUI.OnMove += PerformMove;
        }

        public void PerformMove(Vector2 direction)
        {
            m_Sphere.transform.Translate(direction);
        }
    }
}
