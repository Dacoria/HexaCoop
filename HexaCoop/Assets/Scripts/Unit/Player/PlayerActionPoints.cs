using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionPoints : HexaEventCallback
{
    [ComponentInject] private PlayerScript playerScript;

    public int PlayerActionsPerTurn => 5 + GetComponents<PlayerExtraAPScript>().Sum(x => x.AdditionalAP);
    public int ActionPointsLimit = 10;

    public int CurrentPlayerAP { get; private set; }

    public void IncreaseAP(int increaseAmount) => CurrentPlayerAP = Mathf.Min(CurrentPlayerAP + increaseAmount, ActionPointsLimit);
    public void DecreaseAP(int decreaseAmount) => CurrentPlayerAP -= decreaseAmount;

    protected override void OnPlayerAbility(PlayerScript player, Hex hex, AbilityType type, int queueId)
    {
        if(!Settings.UseQueueAbilities)
        {
            UpdateOnPlayerAbilityUsed(player, type);
        }        
    }

    protected override void OnPlayerAbilityQueue(PlayerScript player, Hex hex, AbilityType type)
    {
        if (Settings.UseQueueAbilities)
        {
            UpdateOnPlayerAbilityUsed(player, type);
        }
    }

    protected override void OnRemoveQueueItem(AbilityQueueItem queueItem)
    {
        if (Settings.UseQueueAbilities && queueItem.Player == playerScript)
        {
            IncreaseAP(queueItem.AbilityType.GetCost());
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
            IncreaseAP(PlayerActionsPerTurn);
        }
    }

    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript currentPlayer)
    {
        CurrentPlayerAP = -2;
        if (currentPlayer == playerScript)
        {
            IncreaseAP(PlayerActionsPerTurn);
        }
    }
}