using System;
using Windows.AbilitiesWindow.AbilitiesTree;
using Windows.AbilitiesWindow.MovingTracker;

namespace Windows.AbilitiesWindow.Window
{
public interface IAbilitiesWindowView
{
    IMovingTracker MovingTracker { get; }
    void Initialize();
    void ShowWarningMessage(string message);
    void Dispose();
    void UpdateCurrentAbilityUpgradePoints(float currentPointsCount);
    void SubscribeOnEarnAbilityPointsButton(Action onEarnButtonPressed);
    void UnsubscribeOnEarnAbilityPointsButton(Action onEarnButtonPressed);
    void SubscribeOnForgetAllAbilitiesButton(Action onForgetAllAbilitiesButtonPressed);
    void UnsubscribeOnForgetAllAbilitiesButton(Action onForgetAllAbilitiesButtonPressed);
    void SubscribeOnBuyAbilityButton(Action onBuyAbilityButtonPressed);
    void UnsubscribeOnBuyAbilityButton(Action onBuyAbilityButtonPressed);
    void SubscribeOnForgetSelectedAbilityButton(Action onForgetSelectedAbilityButtonPressed);
    void UnsubscribeOnForgetSelectedAbilityButton(Action onForgetSelectedAbilityButtonPressed);
    IAbilitiesTreeView CreateAbilityTree();
}
}