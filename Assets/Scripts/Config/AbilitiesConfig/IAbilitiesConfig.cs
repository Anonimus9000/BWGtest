namespace Config.AbilitiesConfig
{
public interface IAbilitiesConfig
{
    int EarnAbilitiesPointsStep { get; }
    IAbilityImage[] AllAbilities { get; }
}
}