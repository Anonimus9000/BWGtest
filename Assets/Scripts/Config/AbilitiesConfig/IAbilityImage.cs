using UnityEngine;

namespace Config.AbilitiesConfig
{
public interface IAbilityImage
{
    bool IsBaseAbility { get; set; }
    string Id { get; }
    Sprite AbilitySprite { get; }
    IAbilityImage[] LinkedAbilities { get; }
    float Price { get; }
}
}