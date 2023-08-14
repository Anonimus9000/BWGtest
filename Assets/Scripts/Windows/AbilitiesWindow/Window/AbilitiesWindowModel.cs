using Config.AbilitiesConfig;
using Config.PlayerConfig;

namespace Windows.AbilitiesWindow.Window
{
public class AbilitiesWindowModel : IAbilitiesWindowModel
{
    private const string BuyWarningMessage = "Insufficient skill points to purchase the ability";
    private const string ForgetAbilityWarningMessage = "The ability cannot be forgotten";

    public IPlayerConfig PlayerConfig { get; }
    public IAbilitiesConfig AbilitiesConfig { get; }

    public AbilitiesWindowModel(IPlayerConfig playerConfig, IAbilitiesConfig abilitiesConfig)
    {
        PlayerConfig = playerConfig;
        AbilitiesConfig = abilitiesConfig;
    }

    public void EarnAbilityPoints()
    {
        var currentUpgradeAbilityPoints = PlayerConfig.CurrentUpgradeAbilityPoints;
        var earnAbilitiesPointsStep = AbilitiesConfig.EarnAbilitiesPointsStep;
        var newCurrentUpgradeAbilityPoints = currentUpgradeAbilityPoints + earnAbilitiesPointsStep;
        
        PlayerConfig.UpdateUpgradeAbilityPoints(newCurrentUpgradeAbilityPoints);
    }

    public string GetBuyWarningMessageLocalized()
    {
        return BuyWarningMessage;
    }

    public string GetForgetAbilityMessageLocalized()
    {
        return ForgetAbilityWarningMessage;
    }
}
}