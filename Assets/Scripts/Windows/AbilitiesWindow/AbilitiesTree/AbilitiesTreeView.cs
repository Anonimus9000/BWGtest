using System;
using Windows.AbilitiesWindow.AbilitiesTree.AbilityNode;
using Windows.AbilitiesWindow.MovingTracker;
using Config.AbilitiesConfig;
using UnityEngine;

namespace Windows.AbilitiesWindow.AbilitiesTree
{
public class AbilitiesTreeView : MonoBehaviour, IAbilitiesTreeView
{
    [SerializeField]
    private AbilityNodeView[] _nodeViews;

    private Vector3 _movingDelta;

    public void Initialize(IMovingTracker movingTracker)
    {
        var trackerListeners = new Action<Vector3>[]
        {
            OnMovingDeltaChanged,
        };
        
        movingTracker.Initialize(trackerListeners);
    }

    private void LateUpdate()
    {
        var currentPosition = transform.position;
        var newPosition = currentPosition + _movingDelta;
        currentPosition = new Vector3(newPosition.x, newPosition.y, currentPosition.z);

        transform.position = currentPosition;
    }

    public IAbilityNodeView[] CreateAbilitiesTree(IAbilityImage[] abilityImages)
    {
        var abilityNodeViews = new IAbilityNodeView[abilityImages.Length];

        for (var i = 0; i < abilityImages.Length; i++)
        {
            var abilityImage = abilityImages[i];
            var abilityNodeView = GetAbilityNodeView(abilityImage.Id);
            abilityNodeViews[i] = abilityNodeView;
        }

        return abilityNodeViews;
    }

    public void Dispose()
    {
        Destroy(gameObject);
    }

    private void OnMovingDeltaChanged(Vector3 movingDelta)
    {
        _movingDelta = movingDelta;
    }

    private AbilityNodeView GetAbilityNodeView(string abilityId)
    {
        foreach (var abilityNodeView in _nodeViews)
        {
            if (abilityNodeView.AbilityId == abilityId)
            {
                return abilityNodeView;
            }
        }

        throw new Exception($"Can't find node by id {abilityId}");
    }
}
}