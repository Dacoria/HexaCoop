using System;
using System.Collections;
using UnityEngine;

public class UnitMovement : HexaEventCallback
{
    [ComponentInject] private Animator animator;
    [ComponentInject] private IUnit unit;

    private Hex originalDestinationHex;

    public void GoToDestination(Hex hex, float duration)
    {
        var endPos = hex.transform.position;
        originalDestinationHex = hex;

        RotateTowardsDestination(endPos, callbackOnFinished: () => MoveToDestination(endPos, duration, callbackOnFinished: OnDestinationReached));
    }

    private void OnDestinationReached()
    {
        unit.SetCurrentHexTile(originalDestinationHex);
        ActionEvents.UnitMovingFinished?.Invoke(unit, originalDestinationHex);
    }

    //// sync op netwerk, zodat je zeker weet dat dit gebeurt
    //protected override void OnUnitMovingFinished(IUnit unitMoved, Hex hex)
    //{
    //    if(unitMoved.Id == unit.Id)
    //    {
    //        if(unit.CurrentHexTile != hex)
    //        {
    //            MoveToDestination(hex.transform.position, 0.1f); // dan maar megasnel; fixen dat dit zo gebeurt
    //            unit.SetCurrentHexTile(hex);
    //        }            
    //    }
    //}

    public void RotateTowardsDestination(Vector3 endPosition, Action callbackOnFinished = null)
    {
        var lerpRotation = gameObject.GetAdd<LerpRotation>();
        lerpRotation.RotateTowardsDestination(endPosition, callbackOnFinished: callbackOnFinished);
    }

    private void MoveToDestination(Vector3 endPosition, float duration, Action callbackOnFinished = null)
    {
        var lerpMovement = gameObject.GetAdd<LerpMovement>();
        lerpMovement.MoveToDestination(endPosition, duration, callbackOnFinished: callbackOnFinished, animator: animator);
    }
}