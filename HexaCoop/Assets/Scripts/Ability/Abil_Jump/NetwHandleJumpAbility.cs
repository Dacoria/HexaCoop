
public class NetwHandleJumpAbility : HexaEventCallback, IAbilityNetworkHandler
{
    public bool CanDoAbility(PlayerScript playerDoingAbility, Hex target)
    {
        return !target.IsObstacle() && !target.HasUnit();
    }

    public void NetworkHandle(PlayerScript playerDoingAbility, Hex target)
    {
        gameObject.GetSet<UnitMovement>().GoToDestination(target, 1.3f);
    }   
}