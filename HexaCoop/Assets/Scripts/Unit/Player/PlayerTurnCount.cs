using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerTurnCount : HexaEventCallback
{   
    [ComponentInject] private PlayerScript playerScript;

    public int TurnCount;
    
    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript currentPlayer)
    {
        TurnCount = 0;
        if(playerScript == currentPlayer)
        {
            TurnCount++;
        }
    }

    protected override void OnNewPlayerTurn(PlayerScript currentPlayer)
    {
        if (playerScript == currentPlayer)
        {
            TurnCount++;
        }
    }
}
