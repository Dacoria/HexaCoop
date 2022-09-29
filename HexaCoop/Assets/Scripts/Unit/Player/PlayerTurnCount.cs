using System.Collections.Generic;

public class PlayerTurnCount : HexaEventCallback
{   
    [ComponentInject] private PlayerScript playerScript;

    public int TurnCount;
    public bool HasEndedTurnForPlayer;

    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript currentPlayer)
    {
        TurnCount = 0;
        
        //foreach (PlayerScript player in allPlayers)
        //{
        //    Textt.GameLocal(player.PlayerName + " " + player.Id + ", index " + player.Index + ", isai " + player.IsAi + ", isalive " + player.IsAlive);
        //}

        if (playerScript == currentPlayer)
        {
            TurnCount++;
            HasEndedTurnForPlayer = false;
        }
    }

    protected override void OnNewPlayerTurn(PlayerScript currentPlayer)
    {
        if (playerScript == currentPlayer)
        {
            TurnCount++;
            HasEndedTurnForPlayer = false;
        }
    }

    protected override void OnEndPlayerTurn(PlayerScript player, List<AbilityQueueItem> abilityQueue)
    {
        if (playerScript == player)
        {
            HasEndedTurnForPlayer = true;
        }
    }
}
