using Photon.Pun;
using System.Collections;
using System.Linq;
using UnityEngine;

public partial class GameHandler : HexaEventCallback
{
    public PlayerScript currentPlayer;
    public PlayerScript CurrentPlayer() => currentPlayer;

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

    private IEnumerator AllPlayersFinishedTurnEventInXSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        NetworkActionEvents.instance.AllPlayersFinishedTurn();
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
        if(GameStatus != GameStatus.ActiveRound)
        {
            return;
        }

        NextPlayerTurn();
    }

    protected override void OnNewPlayerTurn(PlayerScript player)
    {
        currentPlayer = player;
    }

    private void NextPlayerTurn()
    {
        do
        {
            currentPlayer = NextPlayer();
        } 
        while (currentPlayer.CurrentHitPoints == 0);

        NetworkActionEvents.instance.NewPlayerTurn(CurrentPlayer());
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

            if (player.PlayerId == currentPlayer.PlayerId)
            {
                foundCurrPlayer = true;
            }
        }

        return AllPlayers[0];
    }
}
