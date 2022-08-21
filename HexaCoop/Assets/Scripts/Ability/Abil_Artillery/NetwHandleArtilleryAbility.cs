using System.Collections;
using System.Linq;
using UnityEngine;

public class NetwHandleArtilleryAbility : HexaEventCallback, IAbilityNetworkHandler
{
    public void NetworkHandle(PlayerScript playerDoingAbility, Hex target)
    {
        if (playerDoingAbility.IsOnMyNetwork())
        {
            target.SetFogOnHex(false); // local!
        }

        var directionOffset = target.HexCoordinates - playerDoingAbility.CurrentHexTile.HexCoordinates;
        var dirIndex = Direction.GetDirectionsList(playerDoingAbility.CurrentHexTile).FindIndex(x => x == directionOffset);

        var waitTimeToSummonRocket = 0f;
        
        while(target != null)
        {
            StartCoroutine(MonoHelper.instance.SummonFallingDamageObject(waitTimeToSummonRocket, target, playerDoingAbility, DamageObjectType.Rocket));
            waitTimeToSummonRocket += 0.2f;

            var newOffset = Direction.GetDirectionsList(target)[dirIndex];
            target = HexGrid.instance.GetTileAt(target.HexCoordinates + newOffset);
        }
    }
}