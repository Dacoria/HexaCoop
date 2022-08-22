using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFireImmumeScript : HexaEventCallback
{
    [ComponentInject] private PlayerScript playerScript;


    private int TurnActivated;
    private int numberOfTurnsActive = 1;
    public int TurnsLeft => TurnActivated + this.numberOfTurnsActive - GameHandler.instance.CurrentTurn;

    private new void Awake()
    {
        base.Awake();
        TurnActivated = GameHandler.instance.CurrentTurn;
    }

    protected override void OnEndPlayerTurn(PlayerScript player)
    {
        if (playerScript == player)
        {
            // beurt van activatie + 1 andere beurten actief!
            if (GameHandler.instance.CurrentTurn >= TurnActivated + this.numberOfTurnsActive)
            {
                Destroy(this);
            }
        }
    }   
}
