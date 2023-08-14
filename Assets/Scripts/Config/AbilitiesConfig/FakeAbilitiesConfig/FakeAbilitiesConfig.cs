using UnityEngine;

namespace Config.AbilitiesConfig.FakeAbilitiesConfig
{
[CreateAssetMenu(fileName = "AbilitiesConfig", menuName = "Configs/AbilitiesConfig/MainConfig")]
public class FakeAbilitiesConfig : ScriptableObject, IAbilitiesConfig
{
    [field: SerializeField]
    public int EarnAbilitiesPointsStep { get; private set; }

    [SerializeField]
    private FakeAbilityImage[] _allAbilities;

    public IAbilityImage[] AllAbilities => _allAbilities;

    private void OnValidate()
    {
        if(_allAbilities == null)
        {
            return;
        }

        if (_allAbilities.Length == 0)
        {
            return;
        }

        for (var i = 0; i < _allAbilities.Length; i++)
        {
            var abilityImage = _allAbilities[i];

            abilityImage.IsBaseAbility = i == 0;
        }
    }
}
}