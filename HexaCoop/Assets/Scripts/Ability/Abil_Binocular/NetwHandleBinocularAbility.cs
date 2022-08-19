public class NetwHandleBinocularAbility : HexaEventCallback, IAbilityNetworkHandler
{
    public void NetworkHandle(PlayerScript playerDoingAbility, Hex target)
    {
        playerDoingAbility.gameObject.AddComponent<PlayerExtraVisionRangeScript>();
    }
}