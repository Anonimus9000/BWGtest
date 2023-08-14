using System.Linq;
using Config.AbilitiesConfig;

namespace Windows.AbilitiesWindow.AbilitiesTree.AbilityNode
{
public class AbilityNodeModel : IAbilityNodeModel
{
    public IAbilityImage AbilityImage { get; }
    public bool IsOwn { get; private set; }
    public bool IsSelected { get; private set; }

    public AbilityNodeModel(IAbilityImage abilityImage, bool isOwn)
    {
        AbilityImage = abilityImage;
        IsOwn = isOwn;
    }
    
    public void UpdateSelectedState(string selectedAbilityId)
    {
        IsSelected = AbilityImage.Id == selectedAbilityId;
    }

    public void UpdateOwnState(string[] ownAbilities)
    {
        IsOwn = ownAbilities.Contains(AbilityImage.Id);
    }
}
}