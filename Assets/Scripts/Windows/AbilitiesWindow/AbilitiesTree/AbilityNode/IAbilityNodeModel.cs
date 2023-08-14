using Config.AbilitiesConfig;

namespace Windows.AbilitiesWindow.AbilitiesTree.AbilityNode
{
public interface IAbilityNodeModel
{
    IAbilityImage AbilityImage { get; }
    bool IsOwn { get; }
    bool IsSelected { get; }
    void UpdateSelectedState(string abilityId);
    void UpdateOwnState(string[] ownAbilities);
}
}