using Config.AbilitiesConfig;
using Config.PlayerConfig;

namespace Windows.AbilitiesWindow.Window
{
public interface IAbilitiesWindowModel
{
    IPlayerConfig PlayerConfig { get; }
    IAbilitiesConfig AbilitiesConfig { get; }
    void EarnAbilityPoints();
    string GetForgetAbilityMessageLocalized();
    string GetBuyWarningMessageLocalized();
}
}