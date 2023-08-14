using UnityEngine;

namespace Config.AbilitiesConfig.FakeAbilitiesConfig
{
[CreateAssetMenu(fileName = "AbilitiesConfig", menuName = "Configs/AbilitiesConfig/AbilityNode")]
public class FakeAbilityImage : ScriptableObject, IAbilityImage
{
    [field: SerializeField]
    public bool IsBaseAbility { get; set; }
    [field: SerializeField]
    public string Id { get; private set; }
    [field: SerializeField]
    public Sprite AbilitySprite { get; private set; }

    [field: SerializeReference]
    public float Price { get; private set; }

    [SerializeField]
    private FakeAbilityImage[] _linkedAbilities;

    public IAbilityImage[] LinkedAbilities => _linkedAbilities;
}
}