using System;

namespace Windows.AbilitiesWindow.AbilitiesTree.AbilityNode
{
public class AbilityNodePresenter : IDisposable

{
    private readonly IAbilityNodeView _abilityNodeView;
    private readonly IAbilityNodeModel _abilityNodeModel;
    private readonly Action<string> _onAbilityPressed;

    public AbilityNodePresenter(IAbilityNodeView abilityNodeView, IAbilityNodeModel abilityNodeModel, Action<string> onAbilityPressed)
    {
        _abilityNodeView = abilityNodeView;
        _abilityNodeModel = abilityNodeModel;
        _onAbilityPressed = onAbilityPressed;
        var abilitySprite = _abilityNodeModel.AbilityImage.AbilitySprite;
        _abilityNodeView.Initialize(abilitySprite, OnAbilityPressed);
        _abilityNodeView.UpdateOwnState(_abilityNodeModel.IsOwn);
    }

    public void Dispose()
    {
        _abilityNodeView.Dispose();
    }
    
    public void UpdateOwnState(string[] ownAbilities)
    {
        _abilityNodeModel.UpdateOwnState(ownAbilities);
        
        var isOwn = _abilityNodeModel.IsOwn;
        UpdateOwnState(isOwn);
    }

    public void UpdateSelectedState(string abilityId)
    {
        _abilityNodeModel.UpdateSelectedState(abilityId);
        
        var isAbilitySelected = _abilityNodeModel.IsSelected;
        _abilityNodeView.UpdateSelectedState(isAbilitySelected);
    }

    private void UpdateOwnState(bool isOwn)
    {
        _abilityNodeView.UpdateOwnState(isOwn);
    }

    private void OnAbilityPressed()
    {
        var abilityImageId = _abilityNodeModel.AbilityImage.Id;
        _onAbilityPressed?.Invoke(abilityImageId);
    }
}
}