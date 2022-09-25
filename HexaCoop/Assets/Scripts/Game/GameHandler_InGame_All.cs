using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class GameHandler : HexaEventCallback
{
    private PlayerScript _currentPlayer;

    public void SetCurrentPlayer(PlayerScript player) => _currentPlayer = player;
    public PlayerScript GetCurrentPlayer() => _currentPlayer;

    protected override void OnEndPlayerTurn(PlayerScript player, List<AbilityQueueItem> abilityQueue)
    {
        if (!PhotonNetwork.IsMasterClient) { return; }
        EndPlayerTurnWithQueue(player, abilityQueue);
    }

    protected override void OnAllPlayersFinishedTurn()
    {
        CurrentTurn++; // Losse event call van maken? eigenlijk is de turn pas geeindigd na de enemy fase...

        if (!PhotonNetwork.IsMasterClient) { return; }

        // 1 doet de monster movements
        if (EnemyManager.instance.GetEnemies().Any())
        {
            DoEnemyFase(0.5f);
        }
        else
        {
            StartNewPlayerTurns();
        }
    }

    private void StartNewPlayerTurns()
    {
        if (!PhotonNetwork.IsMasterClient) { return; }
        NetworkAE.instance.NewPlayerTurn_Simultanious();
    }

    protected override void OnNewPlayerTurn(PlayerScript player)
    {
        SetNewPlayerOrderForSimultaniousTurns();

        if (player.IsOnMyNetwork())
        {
            // alleen op eigen netwerk setten (om zo ook AI te ondersteunen; vandaar zo)
            SetCurrentPlayer(player);
        }
    }
}
