using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerAi : HexaEventCallback
{
    [ComponentInject] private PlayerScript player;
    private PlayerAiMove playerAiMove;

    private new void Awake()
    {
        this.ComponentInject();
        this.playerAiMove = gameObject.AddComponent<PlayerAiMove>();
    }        
    protected override void OnNewRoundStarted(List<PlayerScript> players, PlayerScript currPlayer)
    {
        StartCoroutine(OnNewTurn(currPlayer));
    }

    protected override void OnNewPlayerTurn(PlayerScript currPlayer)
    {
        StartCoroutine(OnNewTurn(currPlayer));
    }

    private IEnumerator OnNewTurn(PlayerScript currPlayer)
    {
        yield return Wait4Seconds.Get(2f); // wacht op wijzigingen verwerken + wachttijd
        if (player.IsMyTurn() && player == currPlayer)
        {
            this.playerAiMove.DoTurn();
        }
    }
}
