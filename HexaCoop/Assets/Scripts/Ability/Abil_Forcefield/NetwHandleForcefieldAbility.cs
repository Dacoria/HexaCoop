
public class NetwHandleForcefieldAbility : HexaEventCallback, IAbilityNetworkHandler
{    
    public void NetworkHandle(PlayerScript player, Hex hexNearMe, Hex target2)
    {
        player.gameObject.AddComponent<PlayerForcefieldScript>();
    }
}