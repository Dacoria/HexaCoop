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

    public PlayerScript GetCurrentPlayer()
    {
        // IETS MET SIMULTANIOUS TURN


        return _currentPlayer;
        //Settings.UseQueueAbilities

        //return currentPlayer;
    }

    protected override void OnEndPlayerTurn(PlayerScript player)
    {
        // 1 iemand bepaalt de volgene stap --> masterclient
        if(PhotonNetwork.IsMasterClient)
        {
            if (NetworkHelper.instance.GetAllPlayers(isAlive: true).Any(x => x.TurnCount < CurrentTurn))
            {
                NextPlayerTurn();
            }
            else
            {
                StartCoroutine(AllPlayersFinishedTurnEventInXSeconds(0.5f)); // geeft iedereen tijd om events te verwerken, voordat de nieuwe komt
            }
        }
    }

    protected override void OnEndPlayerTurnWithQueue(PlayerScript player, List<AbilityQueueItem> abilityQueue)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            var waitTime = 0.5f;
            foreach (AbilityQueueItem abilityQueueItem in abilityQueue)
            {
                StartCoroutine(InitAbilityInXSeconds(waitTime, abilityQueueItem));
                waitTime += 2f;
            }

            // SIMULTANIOUS TURNS?
            //StartCoroutine(AllPlayersFinishedTurnEventInXSeconds(waitTime));
            StartCoroutine(EndPlayerTurnInXSeconds(waitTime, player));
        }
    }

    private IEnumerator EndPlayerTurnInXSeconds(float waitTime, PlayerScript player)
    {
        yield return Wait4Seconds.Get(waitTime);
        NetworkAE.instance.Invoker_EndPlayerTurn(player);
    }

    private IEnumerator InitAbilityInXSeconds(float waitTime, AbilityQueueItem abilityQueueItem)
    {
        yield return Wait4Seconds.Get(waitTime);
        NetworkAE.instance.Invoker_PlayerAbility(abilityQueueItem.Player, abilityQueueItem.Hex, abilityQueueItem.AbilityType);
    }

    private IEnumerator AllPlayersFinishedTurnEventInXSeconds(float seconds)
    {
        yield return Wait4Seconds.Get(seconds);
        NetworkAE.instance.AllPlayersFinishedTurn();
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
                NextPlayerTurn();
            }
        }
    }

    private void EnemyFaseFinished()
    {
        if(GameStatus != GameStatus.PlayerFase)
        {
            return;
        }

        NextPlayerTurn();
    }

    protected override void OnNewPlayerTurn(PlayerScript player)
    {
        SetCurrentPlayer(player);
    }

    private void NextPlayerTurn()
    {
        do
        {
            SetCurrentPlayer(NextPlayer());
        } 
        while (Netw.CurrPlayer().CurrentHP == 0);

        NetworkAE.instance.NewPlayerTurn(Netw.CurrPlayer());
    }

    private PlayerScript NextPlayer()
    {
        var foundCurrPlayer = false;        

        for (int i = 0; i < AllPlayers.Count(); i++)
        {
            var player = AllPlayers[i];
            if (foundCurrPlayer)
            {
                return player;
            }

            if (player.Id == Netw.CurrPlayer().Id)
            {
                foundCurrPlayer = true;
            }
        }

        return AllPlayers[0];
    }
}
