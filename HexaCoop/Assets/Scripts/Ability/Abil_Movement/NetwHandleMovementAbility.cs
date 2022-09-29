
public class NetwHandleMovementAbility : HexaEventCallback, IAbilityNetworkHandler
{
    public bool CanDoAbility(PlayerScript playerDoingAbility, Hex target, Hex target2)
    {
        return !target.IsObstacle();
    }

    public void NetworkHandle(PlayerScript playerDoingAbility, Hex target, Hex target2)
    {
        if (target.HasUnit(isAlive: true))
        {
            gameObject.GetAdd<UnitAttack>().AttackUnitOnHex(target);
        }
        else
        {
            gameObject.GetAdd<UnitMovement>().GoToDestination(target, 1.3f);
        }
    }   
}