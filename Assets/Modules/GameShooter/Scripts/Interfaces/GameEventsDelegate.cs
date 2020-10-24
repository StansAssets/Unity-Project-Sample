using UnityEngine;

namespace Game.GamePlay
{
    abstract class GameEventsDelegate : MonoBehaviour, IGameEventsDelegate
    {
        public abstract void OnGameInitialized();
    }
}
