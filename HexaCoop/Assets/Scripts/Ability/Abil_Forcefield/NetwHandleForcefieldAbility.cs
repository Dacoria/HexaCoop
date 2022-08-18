
public class NetwHandleForcefieldAbility : HexaEventCallback, IAbilityNetworkHandler
{    
    public void NetworkHandle(PlayerScript player, Hex hexNearMe)
    {
        player.gameObject.AddComponent<PlayerForcefieldScript>();
    }
}