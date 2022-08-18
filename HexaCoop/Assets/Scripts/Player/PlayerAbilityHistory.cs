using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityHistory : HexaEventCallback
{
    [ComponentInject] private PlayerScript playerScript;

    public Dictionary<int, List<AbilityType>> AbilitiesPerTurn;

    public int AbilityDoneThisTurnCount(AbilityType type) =>
        AbilitiesPerTurn.ContainsKey(GameHandler.instance.CurrentTurn) ?
        AbilitiesPerTurn[GameHandler.instance.CurrentTurn].Count(x => x == type)
        : 0;

    public int AbilityDonePreviousTurnCount(AbilityType type) =>
        AbilitiesPerTurn.ContainsKey(GameHandler.instance.CurrentTurn - 1) ?
        AbilitiesPerTurn[GameHandler.instance.CurrentTurn - 1].Count(x => x == type)
        : 0;

    public bool HasDoneAbilityThisTurn(AbilityType type) => AbilityDoneThisTurnCount(type) > 0;
    public bool HasDoneAbilityPreviousTurn(AbilityType type) => AbilityDonePreviousTurnCount(type) > 0;

    protected override void OnNewPlayerTurn(PlayerScript player)
    {
        if (player == playerScript) 
        {
            AbilitiesPerTurn = new Dictionary<int, List<AbilityType>>();
            AbilitiesPerTurn.Add(GameHandler.instance.CurrentTurn, new List<AbilityType>());
        }
    }

    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript player)
    {
        if (player == playerScript)
        {
            AbilitiesPerTurn = new Dictionary<int, List<AbilityType>>();
            AbilitiesPerTurn.Add(GameHandler.instance.CurrentTurn, new List<AbilityType>());
        }
    }

    protected override void OnPlayerAbility(PlayerScript player, Hex hex, AbilityType abilityType)
    {
        if (player == playerScript)
        {
            AbilitiesPerTurn[GameHandler.instance.CurrentTurn].Add(abilityType);
        }
    }
}