using System;
using Windows.AbilitiesWindow.AbilitiesTree;

namespace Windows.AbilitiesWindow.Window
{
public class AbilitiesWindowPresenter : IDisposable
{
    private readonly IAbilitiesWindowModel _abilitiesWindowModel;
    private readonly IAbilitiesWindowView _abilitiesWindowView;
    private readonly AbilitiesTreePresenter _abilityTreePresenter;

    public AbilitiesWindowPresenter(
        IAbilitiesWindowModel abilitiesWindowModel,
        IAbilitiesWindowView abilitiesWindowView)
    {
        _abilitiesWindowModel = abilitiesWindowModel;
        _abilitiesWindowView = abilitiesWindowView;

        var abilitiesTreeView = _abilitiesWindowView.CreateAbilityTree();
        var playerConfig = _abilitiesWindowModel.PlayerConfig;
        var abilitiesConfig = _abilitiesWindowModel.AbilitiesConfig;
        var abilitiesTreeModel = new AbilitiesTreeModel(playerConfig, abilitiesConfig);
        var movingTracker = _abilitiesWindowView.MovingTracker;
        _abilityTreePresenter = new AbilitiesTreePresenter(abilitiesTreeView, abilitiesTreeModel, movingTracker);
        _abilitiesWindowView.Initialize();

        SubscribeOnViewEvents();
        UpdatePointsCounter();
    }

    public void Dispose()
    {
        UnsubscribeOnViewEvents();
        
        _abilitiesWindowView.Dispose();
        _abilityTreePresenter.Dispose();
    }

    private void SubscribeOnViewEvents()
    {
        _abilitiesWindowView.SubscribeOnEarnAbilityPointsButton(OnEarnAbilityPointsButtonPressed);
        _abilitiesWindowView.SubscribeOnForgetAllAbilitiesButton(OnForgetAllAbilitiesButtonPressed);
        _abilitiesWindowView.SubscribeOnBuyAbilityButton(OnBuyAbilityButtonPressed);
        _abilitiesWindowView.SubscribeOnForgetSelectedAbilityButton(OnForgetSelectedAbilityButtonPressed);
    }

    private void UnsubscribeOnViewEvents()
    {
        _abilitiesWindowView.UnsubscribeOnEarnAbilityPointsButton(OnEarnAbilityPointsButtonPressed);
        _abilitiesWindowView.UnsubscribeOnForgetAllAbilitiesButton(OnForgetAllAbilitiesButtonPressed);
        _abilitiesWindowView.UnsubscribeOnBuyAbilityButton(OnBuyAbilityButtonPressed);
        _abilitiesWindowView.UnsubscribeOnForgetSelectedAbilityButton(OnForgetSelectedAbilityButtonPressed);
    }

    private void OnForgetSelectedAbilityButtonPressed()
    {
        if (_abilityTreePresenter.TryForgetSelectedAbility())
        {
            UpdatePointsCounter();
        }
        else
        {
            ShowForgetAbilityWarningMessage();
        }
    }

    private void OnEarnAbilityPointsButtonPressed()
    {
        _abilitiesWindowModel.EarnAbilityPoints();
        UpdatePointsCounter();
    }

    private void OnForgetAllAbilitiesButtonPressed()
    {
        _abilityTreePresenter.ForgetAllAbilities();
        UpdatePointsCounter();
    }

    private void OnBuyAbilityButtonPressed()
    {
        if (!_abilityTreePresenter.TryBuySelectedAbility())
        {
            ShowBuyWarningMessage();
        }
        else
        {
            UpdatePointsCounter();
        }
    }

    private void ShowForgetAbilityWarningMessage()
    {
        var forgetAbilityMessageLocalized = _abilitiesWindowModel.GetForgetAbilityMessageLocalized();
        ShowWarningMessage(forgetAbilityMessageLocalized);
    }
    
    private void ShowBuyWarningMessage()
    {
        var buyWarningMessageLocalized = _abilitiesWindowModel.GetBuyWarningMessageLocalized();
        ShowWarningMessage(buyWarningMessageLocalized);
    }

    private void ShowWarningMessage(string message)
    {
        _abilitiesWindowView.ShowWarningMessage(message);
    }

    private void UpdatePointsCounter()
    {
        var currentUpgradeAbilityPoints = _abilitiesWindowModel.PlayerConfig.CurrentUpgradeAbilityPoints;
        _abilitiesWindowView.UpdateCurrentAbilityUpgradePoints(currentUpgradeAbilityPoints);
    }
}
}