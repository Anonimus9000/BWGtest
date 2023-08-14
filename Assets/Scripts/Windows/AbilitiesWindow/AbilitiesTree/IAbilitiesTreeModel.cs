using System.Collections.Generic;
using Config.AbilitiesConfig;

namespace Windows.AbilitiesWindow.AbilitiesTree
{
public interface IAbilitiesTreeModel
{
    IAbilityImage[] AllAbilities { get; }
    List<string> OwnAbilitiesIds { get; }
    bool IsOwnAbility(string abilityId);
    void ForgetAllAbilities();
    bool TryBuySelectedAbility();
    bool TryForgetSelectedAbility();
    void UpdateSelectedAbility(string selectedAbility);
}
}