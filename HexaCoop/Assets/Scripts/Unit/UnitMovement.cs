using System;
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
        ActionEvents.UnitMovingFinished?.Invoke(unit);
    }

    public void RotateTowardsDestination(Vector3 endPosition, Action callbackOnFinished = null)
    {
        var lerpRotation = gameObject.GetSet<LerpRotation>();
        lerpRotation.RotateTowardsDestination(endPosition, callbackOnFinished: callbackOnFinished);
    }

    private void MoveToDestination(Vector3 endPosition, float duration, Action callbackOnFinished = null)
    {
        var lerpMovement = gameObject.GetSet<LerpMovement>();
        lerpMovement.MoveToDestination(endPosition, duration, callbackOnFinished: callbackOnFinished, animator: animator);
    }
}