public class VictoryScript : HexaEventCallback
{
    public Hex VictoryHex;   

    protected override void OnPlayerAbility(PlayerScript player, Hex hex, AbilityType abilityType)
    {
        if(abilityType == AbilityType.Movement)
        {
            if(hex.HexCoordinates == VictoryHex.HexCoordinates)
            {
                Textt.GameLocal("Victory! " + player.PlayerName + " has reached the middle!");
                ActionEvents.EndRound?.Invoke(true, player);
            }
        }
    }
}