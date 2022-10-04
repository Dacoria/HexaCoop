using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerActionPoints : HexaEventCallback
{
    [ComponentInject] private PlayerScript playerScript;

    public int ActionPointsLimit { get; private set; }
    public int CurrentPlayerAP { get; private set; }

    public void IncreaseAP(int increaseAmount) => CurrentPlayerAP = Mathf.Min(CurrentPlayerAP + increaseAmount, ActionPointsLimit);
    public void DecreaseAP(int decreaseAmount) => CurrentPlayerAP -= decreaseAmount;
       
    protected override void OnPlayerAbilityQueue(PlayerScript player, Hex hex, Hex hex2, AbilityType type)
    {
        UpdateOnPlayerAbilityUsed(player, type);
    }

    protected override void OnRemoveQueueItems(List<AbilityQueueItem> queueItems)
    {
        foreach (var queueItem in queueItems)
        {
            if (queueItem.Player == playerScript)
            {
                IncreaseAP(queueItem.AbilityType.GetCost());
            }
        }
    }

    private void UpdateOnPlayerAbilityUsed(PlayerScript player, AbilityType type)
    {
        if (player == playerScript)
        {
            DecreaseAP(type.GetCost());
        }
    }

    protected override void OnNewPlayerTurn(PlayerScript player)
    {
        if (player == playerScript)
        {
            CurrentPlayerAP = ActionPointsLimit;
        }
    }

    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript currentPlayer)
    {
        ActionPointsLimit = LevelSettings.GetActionPointsEachTurn();
        CurrentPlayerAP = 0;
        if (currentPlayer == playerScript)
        {
            CurrentPlayerAP = ActionPointsLimit;
        }
    }
}