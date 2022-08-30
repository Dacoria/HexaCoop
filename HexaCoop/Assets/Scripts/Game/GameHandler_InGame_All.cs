using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class GameHandler : HexaEventCallback
{
    private PlayerScript _currentPlayer;

    public void SetCurrentPlayer(PlayerScript player)
    {
        _currentPlayer = player;
    }

    protected override void OnEndPlayerTurn(PlayerScript player, List<AbilityQueueItem> abilityQueue)
    {
        if (Settings.UseQueueAbilities)
        {
            EndPlayerTurnWithQueue(player, abilityQueue);
        }
        else
        {
            EndPlayerTurnSequential(player);
        }
    }

    public PlayerScript GetCurrentPlayer()
    {
        if (Settings.UseSimultaniousTurns)
        {
            Netw.MyPlayer();
        }


        return _currentPlayer;        
    }   

    protected override void OnAllPlayersFinishedTurn()
    {
        CurrentTurn++; // Losse event call van maken? eigenlijk is de turn pas geeindigd na de enemy fase...

        // 1 doet de monster movements
        if (PhotonNetwork.IsMasterClient)
        {
            if(EnemyManager.instance.GetEnemies().Any())
            {
                DoEnemyFase(0.5f); 
            }
            else
            {
                StartNewPlayerTurns();
            }
        }
    }

    private void StartNewPlayerTurns()
    {
        if (Settings.UseSimultaniousTurns)
        {
            NetworkAE.instance.NewPlayerTurn(null);
        }
        else
        {
            NextPlayerTurn();
        }
    }
}
