
public class NetwHandleJumpAbility : HexaEventCallback, IAbilityNetworkHandler
{
    public bool CanDoAbility(PlayerScript playerDoingAbility, Hex target, Hex target2)
    {
        return !target.IsObstacle() && !target.HasUnit(isAlive: true);
    }

    public void NetworkHandle(PlayerScript playerDoingAbility, Hex target, Hex target2)
    {
        gameObject.GetAdd<UnitMovement>().GoToDestination(target, 1.3f);
    }   
}