using System;
using UnityEngine;

namespace Windows.AbilitiesWindow.AbilitiesTree.AbilityNode
{
public interface IAbilityNodeView : IDisposable
{
    void Initialize(Sprite abilityIcon, Action onAbilityPressed);
    void UpdateSelectedState(bool isSelected);
    void UpdateOwnState(bool isOwn);
}
}