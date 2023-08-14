using System.Collections.Generic;

namespace Config.PlayerConfig
{
public interface IPlayerConfig
{
    string PlayerId { get; }
    List<string> OwnAbilitiesIds { get; }
    float CurrentUpgradeAbilityPoints { get; }
    void UpdateUpgradeAbilityPoints(float newPointsCount);
    void RemoveFromOwnAbility(string abilityId);
    void AddToOwnAbility(string abilityId);
    void ClearOwnAbilities();
    void SubtractPoints(float amountToSubtract);
    void AddPoints(float amount);
}
}