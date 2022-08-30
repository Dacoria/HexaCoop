using System.Linq;

public static class AbilityExtensions
{
    public static int Cost(this AbilityType abilityType) => abilityType == AbilityType.None ? 0 : AbilitySetup.AbilitySettings.Single(x => x.Type == abilityType).Cost;
    public static int AvailableFromTurn(this AbilityType abilityType) => abilityType == AbilityType.None ? 0 : AbilitySetup.AbilitySettings.Single(x => x.Type == abilityType).AvailableFromTurn;
    public static bool EventImmediatelyFinished(this AbilityType abilityType) => abilityType == AbilityType.None ? true : AbilitySetup.AbilitySettings.Single(x => x.Type == abilityType).EventImmediatelyFinished;
    public static bool IsAvailableInGame(this AbilityType abilityType) => abilityType == AbilityType.None ? false : abilityType.AvailableFromTurn() <= 100;
    public static bool IsPickup(this AbilityType abilityType) => abilityType == AbilityType.None ? true : AbilitySetup.AbilitySettings.Single(x => x.Type == abilityType).IsPickup;


    public static bool IsAvailableThisTurn(this AbilityType abilityType, PlayerScript player) => 
        abilityType == AbilityType.None ? true : player.TurnCount >= abilityType.AvailableFromTurn();

    public static bool IsAvailableThisTurn(this AbilityType abilityType)
    {
        var player = Netw.CurrPlayer();
        return abilityType.IsAvailableThisTurn(player);
    }

    public static bool HaveEnoughPoints(this AbilityType abilityType, PlayerScript player) => 
        abilityType == AbilityType.None ? true : player.CurrentAP >= abilityType.Cost();

    public static bool HaveEnoughPoints(this AbilityType abilityType)
    {
        var player = Netw.CurrPlayer();
        return player.CurrentAP >= abilityType.Cost();
    }

    public static bool IsAvailable(this AbilityType abilityType, PlayerScript player) => 
        abilityType.IsAvailableThisTurn(player) && abilityType.HaveEnoughPoints(player);   
}