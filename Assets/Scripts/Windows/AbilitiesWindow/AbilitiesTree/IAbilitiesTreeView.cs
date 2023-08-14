using Windows.AbilitiesWindow.AbilitiesTree.AbilityNode;
using Windows.AbilitiesWindow.MovingTracker;
using Config.AbilitiesConfig;

namespace Windows.AbilitiesWindow.AbilitiesTree
{
public interface IAbilitiesTreeView
{
    void Initialize(IMovingTracker movingTracker);
    IAbilityNodeView[] CreateAbilitiesTree(IAbilityImage[] abilityImages);
    void Dispose();
}
}