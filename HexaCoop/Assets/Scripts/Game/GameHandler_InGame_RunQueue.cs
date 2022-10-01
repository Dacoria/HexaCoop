using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class GameHandler : HexaEventCallback
{
    protected override void OnStartAbilityQueue(List<AbilityQueueItem> abilityQueueItems)
    {        
        if (!PhotonNetwork.IsMasterClient) { return; }
        
        var waitTime = 0.5f;
        foreach (var abilityQueueItem in abilityQueueItems)
        {
            StartCoroutine(InitAbilityInXSeconds(waitTime, abilityQueueItem));
            waitTime += abilityQueueItem.AbilityType.GetDuration() + 0.3f;
        }

        StartCoroutine(EndQueuePlayerTurnInXSeconds(waitTime));
    }

    private IEnumerator InitAbilityInXSeconds(float waitTime, AbilityQueueItem abilityQueueItem)
    {
        var hexCoorPlayerStartTurn = abilityQueueItem.Player.CurrentHexTile.HexCoordinates;
        if (!PhotonNetwork.IsMasterClient) { yield break; }
        yield return Wait4Seconds.Get(waitTime);
        abilityQueueItem.UpdateHexByCoor(); // preventie voor tile swappen

        if (!abilityQueueItem.Player.IsAlive || GameStatus == GameStatus.RoundEnded)
        {
            // player kan dood zijn --> in dat geval, geen acties van hem meer
            NetworkAE.instance.PlayerAbilityNotExecuted(abilityQueueItem.Player, abilityQueueItem.Hex, abilityQueueItem.Hex2, abilityQueueItem.AbilityType, abilityQueueItem.Id);
            yield break;
        }
        
        var hexForAbil = DetermineHexForAbil(abilityQueueItem.Player, abilityQueueItem.AbilityType, abilityQueueItem.Hex, hexCoorPlayerStartTurn);
        if (hexForAbil != null && abilityQueueItem.Player.CanDoAbility(hexForAbil, abilityQueueItem.Hex2, abilityQueueItem.AbilityType))
        {
            NetworkAE.instance.Invoker_PlayerAbility(abilityQueueItem.Player, hexForAbil, abilityQueueItem.Hex2, abilityQueueItem.AbilityType, abilityQueueItem.Id);
        }
        else
        {
            var hexToSend = hexForAbil ?? abilityQueueItem.Hex;
            NetworkAE.instance.PlayerAbilityNotExecuted(abilityQueueItem.Player, hexToSend, abilityQueueItem.Hex2, abilityQueueItem.AbilityType, abilityQueueItem.Id);
        }        
    }

    private Hex DetermineHexForAbil(PlayerScript player, AbilityType abilityType, Hex hexSubmitted, Vector3Int hexCoorPlayerStartTurn)
    {
        // voor movement --> directie ipv hex-resultaat --> hier bepalen (voor nu)
        if (player.CurrentHexTile.HexCoordinates == hexCoorPlayerStartTurn)
        {
            return hexSubmitted;
        }
        if(!abilityType.GetTargetHexIsRelativeToPlayer())
        {
            return hexSubmitted;
        }

        var directions = hexCoorPlayerStartTurn.DeriveDirections(hexSubmitted.HexCoordinates);

        var newTargetHexCoor = player.CurrentHexTile.HexCoordinates.GetNewHexCoorFromDirections(directions);
        var hexResult = newTargetHexCoor.GetHex();

        return hexResult;
    }

    private IEnumerator EndQueuePlayerTurnInXSeconds(float waitTime)
    {
        if (!PhotonNetwork.IsMasterClient) { yield break; }
        yield return Wait4Seconds.Get(waitTime);

        NetworkAE.instance.AllPlayersFinishedTurn();
    }
}
