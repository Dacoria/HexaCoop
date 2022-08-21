using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetCameraPosition : HexaEventCallback
{
    private Vector3 originalCameraPosition;
    private Quaternion originalCameraRotation;

    private Vector3 cameraPlayerOffset = new Vector3(0, 14f, -8f);   

    new void Start()
    {
        base.Start();
        originalCameraPosition = Camera.main.transform.position;
        originalCameraRotation = Camera.main.transform.rotation;
    }

    protected override void OnNewRoundStarted(List<PlayerScript> arg1, PlayerScript arg2)
    {
        // begin het spel bij je eigen speler
        StartCoroutine(ResetCameraAfterXSeconds(0.15f));
    }

    private IEnumerator ResetCameraAfterXSeconds(float seconds)
    {
        // wacht op spel laden
        yield return new WaitForSeconds(seconds);
        ResetCameraToPlayer();
    }

    public void ResetCameraToPlayer()
    {
        // AI + aan de beurt? pak die, anders pak je eigen char
        var currPlayer = Netw.CurrPlayer().IsOnMyNetwork() ? Netw.CurrPlayer() : Netw.MyPlayer();

        // nog niet begonnen? pak start-pos. Wel begonnen? Pak speler pos
        var targetPos = currPlayer?.CurrentHexTile != null ?
            currPlayer.CurrentHexTile.transform.position + this.cameraPlayerOffset :
            originalCameraPosition;

        // geleidelijk bewegen + draaien naar target plek+rot
        var lerpMovement = gameObject.GetSet<LerpMovement>();
        lerpMovement.MoveToDestination(endPosition: targetPos, duration: 0.6f, destroyGoOnFinished: false);

        var lerpRotation = gameObject.GetSet<LerpRotation>();
        lerpRotation.RotateTowardsAngle(endRotation: originalCameraRotation, duration: 0.6f, destroyGoOnFinished: false);
    }
}