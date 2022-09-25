
using System;
using System.Collections.Generic;
using UnityEngine;

public class TemperaryMountainScript : HexaEventCallback
{
    private int TurnActivated;

    [ComponentInject] private Hex hex;

    public PlayerScript PlayerThatSummonedMountain;

    private new void Awake()
    {
        base.Awake();
        TurnActivated = GameHandler.instance.CurrentTurn;
    }

    protected override void OnAllPlayersFinishedTurn()
    {
        if(Settings.UseSimultaniousTurns)
        {
            if (GameHandler.instance.CurrentTurn >= TurnActivated + 2) // 2 beurten, door sim turns
            {
                DestroyMountain();
            }
        }
    }

    protected override void OnEndPlayerTurn(PlayerScript player, List<AbilityQueueItem> abilityQueue)
    {
        if (!Settings.UseSimultaniousTurns && PlayerThatSummonedMountain == player)
        {
            if (GameHandler.instance.CurrentTurn >= TurnActivated + 1)
            {
                DestroyMountain();
            }
        }
    }

    private void DestroyMountain()
    {     
        var mountainGo = Utils.GetStructureGoFromHex(hex);

        if (mountainGo != null)
        {
            var mountainCopy = Instantiate(mountainGo, mountainGo.transform.position, Quaternion.identity);
            var lerpMovement = mountainCopy.GetAdd<LerpMovement>();
            lerpMovement.MoveDown(distance: 2, duration: 2, destroyGoOnFinished: true);
        }

        hex.ChangeHexStructureType(HexStructureType.None);
        Destroy(this);        
    }
}
