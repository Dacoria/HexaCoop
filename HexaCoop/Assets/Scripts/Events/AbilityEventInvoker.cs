public static class AbilityEventInvoker
{
    public static void PlayerAbility(PlayerScript player, Hex hex, AbilityType abilityType)
    {
        ActionEvents.PlayerAbilityQueue?.Invoke(player, hex, abilityType);
    }

    public static void Ability(this PlayerScript player, Hex hex, AbilityType abilityType) => PlayerAbility(player, hex, abilityType);
}