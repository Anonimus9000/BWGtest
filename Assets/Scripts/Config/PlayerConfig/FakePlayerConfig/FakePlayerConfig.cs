using System.Collections.Generic;
using UnityEngine;

namespace Config.PlayerConfig.FakePlayerConfig
{
[CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/PlayerConfig")]
public class FakePlayerConfig : ScriptableObject, IPlayerConfig
{
    [SerializeField]
    private FakePlayerImage _fakePlayerImage;
    public IPlayerImage PlayerImage => _fakePlayerImage;

    public string PlayerId => _fakePlayerImage.Id;
    public List<string> OwnAbilitiesIds => _fakePlayerImage.OwnAbilitiesIds;
    public float CurrentUpgradeAbilityPoints => _fakePlayerImage.CurrentUpgradeAbilityPoints;

    public void UpdateUpgradeAbilityPoints(float newPointsCount)
    {
        _fakePlayerImage.CurrentUpgradeAbilityPoints = newPointsCount;
    }

    public void RemoveFromOwnAbility(string abilityId)
    {
        _fakePlayerImage.OwnAbilitiesIds.Remove(abilityId);
    }

    public void AddToOwnAbility(string abilityId)
    {
        _fakePlayerImage.OwnAbilitiesIds.Add(abilityId);
    }

    public void ClearOwnAbilities()
    {
        var ownAbilitiesIds = PlayerImage.OwnAbilitiesIds;
        ownAbilitiesIds.RemoveRange(1, ownAbilitiesIds.Count - 1);
    }

    public void SubtractPoints(float amountToSubtract)
    {
        _fakePlayerImage.CurrentUpgradeAbilityPoints -= amountToSubtract;
    }

    public void AddPoints(float amount)
    {
        _fakePlayerImage.CurrentUpgradeAbilityPoints += amount;
    }
}
}