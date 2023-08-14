using System.Collections.Generic;

namespace Config.PlayerConfig
{
public interface IPlayerImage
{
    string Id { get; }
    List<string> OwnAbilitiesIds { get; }
    float CurrentUpgradeAbilityPoints { get; }
}
}