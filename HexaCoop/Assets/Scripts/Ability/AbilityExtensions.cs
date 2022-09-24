using System.Linq;

public static class AbilityExtensions
{
    public static int GetCost(this AbilityType abilityType) => abilityType == AbilityType.None ? 0 : AbilitySetup.AbilitySettings.Single(x => x.Type == abilityType).Cost;
    public static int GetAvailableFromTurn(this AbilityType abilityType) => abilityType == AbilityType.None ? 0 : AbilitySetup.AbilitySettings.Single(x => x.Type == abilityType).AvailableFromTurn;
    public static int GetAvailableFromQueuePlace(this AbilityType abilityType) => abilityType == AbilityType.None ? 0 : AbilitySetup.AbilitySettings.Single(x => x.Type == abilityType).AvailableFromQueuePlace;
    public static bool GetEventImmediatelyFinished(this AbilityType abilityType) => abilityType == AbilityType.None ? true : AbilitySetup.AbilitySettings.Single(x => x.Type == abilityType).EventImmediatelyFinished;
    public static bool IsAvailableInGame(this AbilityType abilityType) => abilityType == AbilityType.None ? false : abilityType.GetAvailableFromTurn() <= 100;
    public static bool IsPickup(this AbilityType abilityType) => abilityType == AbilityType.None ? true : AbilitySetup.AbilitySettings.Single(x => x.Type == abilityType).IsPickup;
    public static bool GetTargetHexIsRelativeToPlayer(this AbilityType abilityType) => abilityType == AbilityType.None ? false : AbilitySetup.AbilitySettings.Single(x => x.Type == abilityType).TargetHexIsRelativeToPlayer;
    public static float GetDuration(this AbilityType abilityType) => abilityType == AbilityType.None ? 0f : AbilitySetup.AbilitySettings.Single(x => x.Type == abilityType).Duration;


    public static bool IsAvailableThisTurn(this AbilityType abilityType, PlayerScript player) => 
        abilityType == AbilityType.None ? true : player?.TurnCount >= abilityType.GetAvailableFromTurn();

    public static bool IsAvailableThisTurn(this AbilityType abilityType)
    {
        var player = Netw.CurrPlayer();
        return abilityType.IsAvailableThisTurn(player);
    }

    public static bool HaveEnoughPoints(this AbilityType abilityType, PlayerScript player) => 
        abilityType == AbilityType.None ? true : player.CurrentAP >= abilityType.GetCost();

    public static bool HaveEnoughPoints(this AbilityType abilityType)
    {
        var player = Netw.CurrPlayer();
        return player?.CurrentAP >= abilityType.GetCost();
    }

    public static bool IsAvailable(this AbilityType abilityType, PlayerScript player) => 
        abilityType.IsAvailableThisTurn(player) && abilityType.HaveEnoughPoints(player);   
}