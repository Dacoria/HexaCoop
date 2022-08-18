using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionPoints : HexaEventCallback
{
    [ComponentInject] private PlayerScript playerScript;

    public int PlayerActionsPerTurn => 5 + GetComponents<PlayerExtraAPScript>().Sum(x => x.AdditionalAP);
    public int ActionPointsLimit = 10;

    public int CurrentPlayerActionPoints;  

    protected override void OnPlayerAbility(PlayerScript player, Hex hex, AbilityType type)
    {
        if(player == playerScript)
        {
            CurrentPlayerActionPoints -= type.Cost();
        }        
    }

    protected override void OnNewPlayerTurn(PlayerScript player)
    {
        if (player == playerScript)
        {
            CurrentPlayerActionPoints = Mathf.Min(CurrentPlayerActionPoints + PlayerActionsPerTurn, ActionPointsLimit);
        }
    }

    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript currentPlayer)
    {
        CurrentPlayerActionPoints = -2;
        if (currentPlayer == playerScript)
        {
            CurrentPlayerActionPoints = Mathf.Min(CurrentPlayerActionPoints + PlayerActionsPerTurn, ActionPointsLimit);
        }
    }
}