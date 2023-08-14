using System;
using System.Collections.Generic;
using Config.AbilitiesConfig;
using Config.PlayerConfig;

namespace Windows.AbilitiesWindow.AbilitiesTree
{
public class AbilitiesTreeModel : IAbilitiesTreeModel
{
    public IAbilityImage[] AllAbilities => _abilitiesConfig.AllAbilities;
    public List<string> OwnAbilitiesIds => _playerConfig.OwnAbilitiesIds;

    private IAbilityImage _selectedAbility;
    private readonly IPlayerConfig _playerConfig;
    private readonly IAbilitiesConfig _abilitiesConfig;
    private readonly List<IAbilityImage> _abilitiesWithOwnParents;
    
    public AbilitiesTreeModel(IPlayerConfig playerConfig, IAbilitiesConfig abilitiesConfig)
    {
        _playerConfig = playerConfig;
        _abilitiesConfig = abilitiesConfig;
        
        _abilitiesWithOwnParents = InitializeAbilitiesWithOwnParents();
    }

    public bool IsOwnAbility(string abilityId)
    {
        return OwnAbilitiesIds.Contains(abilityId);
    }

    public bool TryForgetSelectedAbility()
    {
        if (_selectedAbility == null)
        {
            return false;
        }

        if (_selectedAbility.IsBaseAbility)
        {
            return false;
        }

        if (!CanForgetAbility(_selectedAbility))
        {
            return false;
        }

        var selectedAbilityPrice = _selectedAbility.Price;
        _playerConfig.AddPoints(selectedAbilityPrice);
        _playerConfig.RemoveFromOwnAbility(_selectedAbility.Id);
        ForgetParentForLinkedAbilities(_selectedAbility);

        return true;
    }

    public void UpdateSelectedAbility(string selectedAbility)
    {
        if (string.IsNullOrEmpty(selectedAbility))
        {
            throw new Exception("Selected ability can't have null or empty id");
        }

        if (!TryGetAbilityById(selectedAbility, out var abilityImage))
        {
            throw new Exception($"Can't find ability by id {selectedAbility}");
        }
        
        _selectedAbility = abilityImage;
    }
    
    public void ForgetAllAbilities()
    {
        foreach (var ownAbilitiesId in _playerConfig.OwnAbilitiesIds)
        {
            if (!TryGetAbilityById(ownAbilitiesId, out var abilityById))
            {
                continue;
            }
            
            _playerConfig.AddPoints(abilityById.Price);
        }
        
        ClearAbilitiesWithOwnParens();

        _playerConfig.ClearOwnAbilities();
    }
    
    public bool TryBuySelectedAbility()
    {
        if (_playerConfig.OwnAbilitiesIds.Contains(_selectedAbility.Id))
        {
            return false;
        }
        
        if (_selectedAbility.IsBaseAbility)
        {
            return false;
        }

        if (!CanBuyAbility(_selectedAbility))
        {
            return false;
        }

        var abilityPrice = _selectedAbility.Price;
        _playerConfig.SubtractPoints(abilityPrice);
        _playerConfig.AddToOwnAbility(_selectedAbility.Id);
        
        SaveParentForLinkedAbilities(_selectedAbility);

        return true;
    }

    private void ClearAbilitiesWithOwnParens()
    {
        var baseAbility = AllAbilities[0];
        if (!baseAbility.IsBaseAbility)
        {
            throw new Exception("The first ability cannot be non-basic");
        }
        
        _abilitiesWithOwnParents.Clear();
        _abilitiesWithOwnParents.AddRange(baseAbility.LinkedAbilities);
    }

    private List<IAbilityImage> InitializeAbilitiesWithOwnParents()
    {
        var abilitiesWithOwnParents = new List<IAbilityImage>(AllAbilities.Length);
        foreach (var abilityImage in AllAbilities)
        {
            if (IsOwnAbility(abilityImage.Id))
            {
                abilitiesWithOwnParents.AddRange(abilityImage.LinkedAbilities);
            }
        }

        return abilitiesWithOwnParents;
    }

    private bool CanBuyAbility(IAbilityImage abilityImage)
    {
        var abilityPrice = abilityImage.Price;
        if (!(_playerConfig.CurrentUpgradeAbilityPoints >= abilityPrice))
        {
            return false;
        }

        if (!_abilitiesWithOwnParents.Contains(abilityImage))
        {
            return false;
        }

        return true;
    }

    private void SaveParentForLinkedAbilities(IAbilityImage parentImage)
    {
        _abilitiesWithOwnParents.AddRange(parentImage.LinkedAbilities);
    }

    private void ForgetParentForLinkedAbilities(IAbilityImage parentImage)
    {
        foreach (var linkedAbility in parentImage.LinkedAbilities)
        {
            _abilitiesWithOwnParents.Remove(linkedAbility);
        }
    }

    private bool CanForgetAbility(IAbilityImage abilityImage)
    {
        foreach (var linkedAbility in abilityImage.LinkedAbilities)
        {
            if (OwnAbilitiesIds.Contains(linkedAbility.Id))
            {
                return false;
            }
        }

        return true;
    }

    private bool TryGetAbilityById(string abilityId, out IAbilityImage abilityById)
    {
        foreach (var abilityImage in _abilitiesConfig.AllAbilities)
        {
            if (abilityImage.Id == abilityId)
            {
                abilityById = abilityImage;
                return true;
            }
        }

        abilityById = null;
        return false;
    }
}
}