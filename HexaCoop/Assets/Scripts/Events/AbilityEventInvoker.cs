public static class AbilityEventInvoker
{
    public static void PlayerAbility(PlayerScript player, Hex hex, Hex hex2, AbilityType abilityType)
    {
        ActionEvents.PlayerAbilityQueue?.Invoke(player, hex, hex2, abilityType);
    }

    public static void Ability(this PlayerScript player, Hex hex, Hex hex2, AbilityType abilityType) => PlayerAbility(player, hex, hex2, abilityType);
    public static void Ability(this PlayerScript player, Hex hex, AbilityType abilityType) => PlayerAbility(player, hex, null, abilityType);
}