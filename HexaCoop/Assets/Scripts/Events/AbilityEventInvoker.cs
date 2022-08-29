public static class AbilityEventInvoker
{
    public static void PlayerAbility(PlayerScript player, Hex hex, AbilityType abilityType)
    {
        if(Settings.UseQueueAbilities)
        {
            ActionEvents.PlayerAbilityQueue?.Invoke(player, hex, abilityType);
        }
        else
        {
            NetworkAE.instance.Invoker_PlayerAbility(player, hex, abilityType);
        }
    }

    public static void Ability(this PlayerScript player, Hex hex, AbilityType abilityType) => PlayerAbility(player, hex, abilityType);
}