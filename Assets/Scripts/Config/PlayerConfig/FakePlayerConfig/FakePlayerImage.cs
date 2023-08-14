using System;
using System.Collections.Generic;
using UnityEngine;

namespace Config.PlayerConfig.FakePlayerConfig
{
[Serializable]
public class FakePlayerImage : IPlayerImage
{
    [field: SerializeField]
    public string Id { get; private set; }
    [field: SerializeField]
    public List<string> OwnAbilitiesIds { get; set; }
    [field: SerializeField]
    public float CurrentUpgradeAbilityPoints { get; set; }
}
}