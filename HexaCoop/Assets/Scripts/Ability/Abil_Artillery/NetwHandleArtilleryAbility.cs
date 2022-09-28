
public class NetwHandleArtilleryAbility : HexaEventCallback, IAbilityNetworkHandler
{
    public void NetworkHandle(PlayerScript playerDoingAbility, Hex target, Hex target2)
    { 
        var direction = playerDoingAbility.CurrentHexTile.HexCoordinates.DeriveDirections(target.HexCoordinates)[0];
        var newTile = playerDoingAbility.CurrentHexTile;

        for (var waitTimeForRocket = 0f; waitTimeForRocket < 3; waitTimeForRocket += 0.2f)
        {
            var newCoor = Direction.GetNewHexCoorFromDirection(newTile.HexCoordinates, direction);
            newTile = HexGrid.instance.GetTileAt(newCoor);

            if (newTile == null)
            {
                break;
            }

            StartCoroutine(MonoHelper.instance.SummonFallingDamageObject(waitTimeForRocket, newTile, playerDoingAbility, DamageObjectType.Rocket));
        }
    }
}