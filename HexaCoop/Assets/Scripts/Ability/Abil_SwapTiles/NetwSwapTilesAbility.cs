
using System;
using System.Collections;
using UnityEngine;

public class NetwHandleSwapTilesAbility : HexaEventCallback, IAbilityNetworkHandler
{
    public bool CanDoAbility(PlayerScript playerDoingAbility, Hex target, Hex target2)
    {        
        return true;
    }

    public void NetworkHandle(PlayerScript playerDoingAbility, Hex target, Hex target2)
    {
        var hexDestination = target2.transform.position;
        var hex2Destination = target.transform.position;

        MoveTileToOtherLoc(target, hexDestination, 1.2f);
        MoveTileToOtherLoc(target2, hex2Destination, 1.2f);
    }

    private void MoveTileToOtherLoc(Hex hexFrom, Vector3 destination, float duration)
    {
        var unitOnTile = UnitManager.instance.GetUnit(hexFrom, isAlive: true);
        if(unitOnTile != null)
        {
            unitOnTile.GameObject.transform.SetParent(hexFrom.gameObject.transform);
        }

        var lerpMovement = hexFrom.gameObject.GetAdd<LerpMovement>();
        lerpMovement.MoveToDestination(destination, duration, callbackOnFinished: () => MoveTileFinished(hexFrom, unitOnTile));
    }

    private void MoveTileFinished(Hex hex, IUnit unitOnTile)
    {
        hex.GetComponent<HexCoordinates>().SetOffSetCoordinates();
        if (unitOnTile != null)
        {
            unitOnTile.GameObject.transform.SetParent(null);
        }
    }
}