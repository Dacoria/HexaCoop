
public class NetwHandleMovementAbility : HexaEventCallback, IAbilityNetworkHandler
{
    public bool CanDoAbility(PlayerScript playerDoingAbility, Hex target)
    {
        return !target.IsObstacle();
    }

    public void NetworkHandle(PlayerScript playerDoingAbility, Hex target)
    {
        if (target.HasUnit())
        {
            gameObject.GetSet<UnitAttack>().AttackUnitOnHex(target);
        }
        else
        {
            gameObject.GetSet<UnitMovement>().GoToDestination(target, 1.3f);
        }
    }   
}