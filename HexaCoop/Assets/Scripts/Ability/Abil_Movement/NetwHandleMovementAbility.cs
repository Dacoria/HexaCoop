
public class NetwHandleMovementAbility : HexaEventCallback, IAbilityNetworkHandler
{
    private Hex newHexTile;

    public void NetworkHandle(PlayerScript playerDoingAbility, Hex target)
    {
        newHexTile = target;
        if (newHexTile.HasUnit())
        {
            gameObject.GetSet<UnitAttack>().AttackUnitOnHex(target);
        }
        else
        {
            gameObject.GetSet<UnitMovement>().GoToDestination(target, 1.3f);
        }
    }   
}