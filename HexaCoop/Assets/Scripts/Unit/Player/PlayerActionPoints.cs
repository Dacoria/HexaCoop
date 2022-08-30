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

    protected override void OnPlayerAbility(PlayerScript player, Hex hex, AbilityType type)
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
            IncreaseAP(queueItem.AbilityType.Cost());
        }
    }

    private void UpdateOnPlayerAbilityUsed(PlayerScript player, AbilityType type)
    {
        if (player == playerScript)
        {
            DecreaseAP(type.Cost());
        }
    }

    protected override void OnNewPlayerTurn(PlayerScript player)
    {
        if (player == playerScript || player == null)
        {
            IncreaseAP(PlayerActionsPerTurn);
        }
    }

    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript currentPlayer)
    {
        CurrentPlayerAP = -2;
        if (currentPlayer == playerScript || currentPlayer == null)
        {
            IncreaseAP(PlayerActionsPerTurn);
        }
    }
}