using System;

namespace Game.GamePlay
{
    interface IInputService
    {
        event Action OnFire;

        float Horizontal { get; }
        float Vertical { get; }
    }
}
