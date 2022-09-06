
public class NetwHandleArtilleryAbility : HexaEventCallback, IAbilityNetworkHandler
{
    public void NetworkHandle(PlayerScript playerDoingAbility, Hex target)
    {
        if (playerDoingAbility.IsOnMyNetwork())
        {
            target.SetFogOnHex(false); // local!
        }


        var direction = playerDoingAbility.CurrentHexTile.HexCoordinates.DeriveDirections(target.HexCoordinates)[0];
        var waitTimeToSummonRocket = 0f;

        var newTile = playerDoingAbility.CurrentHexTile;

        while (true)
        {
            var newCoor = Direction.GetNewHexCoorFromDirection(newTile.HexCoordinates, direction);
            newTile = HexGrid.instance.GetTileAt(newCoor);

            if (newTile == null)
            {
                break;
            }

            StartCoroutine(MonoHelper.instance.SummonFallingDamageObject(waitTimeToSummonRocket, newTile, playerDoingAbility, DamageObjectType.Rocket));
            waitTimeToSummonRocket += 0.2f;
        }
    }
}