using System;
using UnityEngine;

namespace Windows.AbilitiesWindow.MovingTracker
{
public interface IMovingTracker
{
    void Initialize(Action<Vector3>[] onDeltaChanged);
}
}