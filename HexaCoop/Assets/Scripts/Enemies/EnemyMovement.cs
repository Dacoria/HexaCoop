using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : HexaEventCallback
{
    [ComponentInject] private EnemyScript enemyScript;

    protected override void OnEnemyMove(EnemyScript enemy, Hex tile)
    {
        if(enemy.Id == enemyScript.Id)
        {
            RotateMoveTowardsPlayer(tile.HexCoordinates);
        }
    }

    private Vector3 destinationV3;
    private Vector3Int newHexCoordinate;

    private void RotateMoveTowardsPlayer(Vector3Int hexCoordinateToMoveTowards)
    {
        var rotateLerp = GetComponent<LerpRotation>() ?? gameObject.AddComponent<LerpRotation>();

        newHexCoordinate = hexCoordinateToMoveTowards;
        destinationV3 = hexCoordinateToMoveTowards.GetHex().transform.position + new Vector3(0, 1, 0);

        rotateLerp.RotateTowardsDestination(endPosition: destinationV3, callbackOnFinished: MoveTowardsDestination);
    }

    private void MoveTowardsDestination()
    {
        var movementLerp = GetComponent<LerpMovement>() ?? gameObject.AddComponent<LerpMovement>();
        movementLerp.MoveToDestination(endPosition: destinationV3, 2f, callbackOnFinished: MovingFinished);
    }

    private void MovingFinished()
    {
        enemyScript.CurrentHexTile = newHexCoordinate.GetHex();
        enemyScript.ActionFinished();
    }
}
