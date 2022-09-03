using Photon.Pun;
using System.Collections;
using System.Linq;

public partial class GameHandler : HexaEventCallback
{
    private void EndPlayerTurnSequential (PlayerScript player)
    {
        if (!PhotonNetwork.IsMasterClient) { return; }

        // 1 iemand bepaalt de volgende stap --> masterclient
        
        if (NetworkHelper.instance.GetAllPlayers(isAlive: true).Any(x => x.TurnCount < CurrentTurn))
        {
            SetNextCurrentPlayer();
            NetworkAE.instance.NewPlayerTurn_Sequential(Netw.CurrPlayer());
        }
        else
        {
            StartCoroutine(AllPlayersFinishedTurnEventInXSeconds(0.5f)); // geeft iedereen tijd om events te verwerken, voordat de nieuwe komt
        }        
    }   

    private IEnumerator AllPlayersFinishedTurnEventInXSeconds(float seconds)
    {
        yield return Wait4Seconds.Get(seconds);
        NetworkAE.instance.AllPlayersFinishedTurn();
    }

    private void SetNextCurrentPlayer()
    {
        if (!PhotonNetwork.IsMasterClient) { return; }

        do
        {
            SetCurrentPlayer(NextPlayer());
        } 
        while (Netw.CurrPlayer().CurrentHP == 0);
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
