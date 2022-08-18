public class NetwHandleVisionAbility : HexaEventCallback, IAbilityNetworkHandler
{
    public void NetworkHandle(PlayerScript playerDoingAbility, Hex target)
    {        
        if (playerDoingAbility.IsOnMyNetwork())
        {
            target.SetFogOnHex(false); // local!
        }

        target.EnableHighlight(HighlightColorType.Yellow);
    }
}