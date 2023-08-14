using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Windows.AbilitiesWindow.MovingTracker
{
public class TreeMovingTracker : MonoBehaviour, IDragHandler, IEndDragHandler, IDisposable, IMovingTracker
{
    private Action<Vector3>[] _onDeltaChangedEvents;
    private Vector2 _lastPosition;

    public void Initialize(Action<Vector3>[] onDeltaChanged)
    {
        _onDeltaChangedEvents = onDeltaChanged;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta;
        if (_lastPosition == Vector2.zero)
        {
            _lastPosition = eventData.position;
        }
        if (!DraggingPositionWasChanged(eventData.position))
        {
            delta = Vector3.zero;
        }
        else
        {
            delta = _lastPosition - eventData.position;
        }

        var invertDelta = InvertVector(delta);
        OnDeltaChanged(invertDelta);
        _lastPosition = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _lastPosition = Vector2.zero;
        OnDeltaChanged(Vector3.zero);
    }

    public void Dispose()
    {
        _onDeltaChangedEvents = null;
    }

    private bool DraggingPositionWasChanged(Vector2 newPosition)
    {
        var xPositionChanged = Mathf.Abs(_lastPosition.x - newPosition.x) > 1.1f;
        var yPositionChanged = Mathf.Abs(_lastPosition.y - newPosition.y) > 1.1f;

        return xPositionChanged || yPositionChanged;
    }

    private void OnDeltaChanged(Vector3 delta)
    {
        foreach (var onDeltaChangedEvent in _onDeltaChangedEvents)
        {
            onDeltaChangedEvent?.Invoke(delta);
        }
    }
    
    private Vector3 InvertVector(Vector3 vector)
    {
        return new Vector3(-vector.x, -vector.y, -vector.z);
    }
}
}