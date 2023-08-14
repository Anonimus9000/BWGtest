using System;
using System.Collections.Generic;
using Windows.AbilitiesWindow.AbilitiesTree.AbilityNode;
using Windows.AbilitiesWindow.MovingTracker;
using Config.AbilitiesConfig;

namespace Windows.AbilitiesWindow.AbilitiesTree
{
public class AbilitiesTreePresenter : IDisposable
{
    private readonly IAbilitiesTreeView _abilitiesTreeView;
    private readonly IAbilitiesTreeModel _abilitiesTreeModel;
    private readonly List<AbilityNodePresenter> _abilityNodePresenters;

    public AbilitiesTreePresenter(
        IAbilitiesTreeView abilitiesTreeView,
        IAbilitiesTreeModel abilitiesTreeModel,
        IMovingTracker movingTracker)
    {
        _abilitiesTreeView = abilitiesTreeView;
        _abilitiesTreeModel = abilitiesTreeModel;

        var allAbilities = _abilitiesTreeModel.AllAbilities;
        _abilityNodePresenters = new List<AbilityNodePresenter>(allAbilities.Length);
        _abilitiesTreeView.Initialize(movingTracker);

        InitializeNodes(allAbilities);
    }

    public void Dispose()
    {
        _abilitiesTreeView.Dispose();
    }

    private void UpdateOwnAbilities()
    {
        UpdateOwnStateNodes(_abilitiesTreeModel.OwnAbilitiesIds.ToArray());
    }

    private void InitializeNodes(IAbilityImage[] abilityImages)
    {
        var baseAbility = abilityImages[0];
        if (!baseAbility.IsBaseAbility)
        {
            throw new Exception("The first ability should have the isBaseAbility flag");
        }
        
        var abilityViews = _abilitiesTreeView.CreateAbilitiesTree(abilityImages);

        InitializeGraphNodes(abilityImages, abilityViews);
    }

    private void InitializeGraphNodes(IAbilityImage[] abilityImages, IAbilityNodeView[] abilitiesNodeViews)
    {
        for (var i = 0; i < abilityImages.Length; i++)
        {
            var abilityImage = abilityImages[i];
            var abilityNodeView = abilitiesNodeViews[i];
            InitializeNode(abilityImage, abilityNodeView);
        }
    }

    private void InitializeNode(IAbilityImage abilityImage, IAbilityNodeView nodeView)
    {
        var isOwnAbility = _abilitiesTreeModel.IsOwnAbility(abilityImage.Id);
        var nodeModel = new AbilityNodeModel(abilityImage, isOwnAbility);
        var nodePresenter = new AbilityNodePresenter(nodeView, nodeModel, OnNodePressed);
        _abilityNodePresenters.Add(nodePresenter);
    }

    private void UpdateOwnStateNodes(string[] ownAbilities)
    {
        foreach (var abilityNodePresenter in _abilityNodePresenters)
        {
            abilityNodePresenter.UpdateOwnState(ownAbilities);
        }
    }

    private void OnNodePressed(string selectedAbility)
    {
        foreach (var nodePresenter in _abilityNodePresenters)
        {
            nodePresenter.UpdateSelectedState(selectedAbility);
        }

        _abilitiesTreeModel.UpdateSelectedAbility(selectedAbility);
    }

    public bool TryForgetSelectedAbility()
    {
        if (!_abilitiesTreeModel.TryForgetSelectedAbility())
        {
            return false;
        }

        UpdateOwnAbilities();
        return true;

    }

    public void ForgetAllAbilities()
    {
        _abilitiesTreeModel.ForgetAllAbilities();
        UpdateOwnAbilities();
    }

    public bool TryBuySelectedAbility()
    {
        if (!_abilitiesTreeModel.TryBuySelectedAbility())
        {
            return false;
        }

        UpdateOwnAbilities();
        return true;
    }
}
}