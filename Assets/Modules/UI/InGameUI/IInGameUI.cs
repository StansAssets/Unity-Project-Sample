using System;
using UnityEngine;

namespace StansAssets.ProjectSample.Core
{
    public interface IInGameUI
    {
        event Action<Vector2> OnMove;
    }
}
