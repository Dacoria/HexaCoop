using Photon.Pun;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public partial class NetworkAE : MonoBehaviour
{ 
    public void EndPlayerTurn(PlayerScript currentPlayer)
    {
        var netwQueue = GetQueueOfPlayer(currentPlayer);
        var abilitiesQueueJson = JsonUtility.ToJson(netwQueue);

        photonView.RPC("RPC_AE_EndPlayerTurn", RpcTarget.All, currentPlayer.Id, abilitiesQueueJson);
    }

    [PunRPC]
    public void RPC_AE_EndPlayerTurn(int currentPlayerId, string abilitiesJson)
    {
        var netwPlayerAbilityQueue = JsonUtility.FromJson<NetwPlayerAbilityQueue>(abilitiesJson);
        var playerAbilityQueue = ConvertToConcreteList(netwPlayerAbilityQueue);

        ActionEvents.EndPlayerTurn?.Invoke(currentPlayerId.GetPlayer(), playerAbilityQueue);
    }

    private NetwPlayerAbilityQueue GetQueueOfPlayer(PlayerScript player) => ConvertToAbilityNetworkList(player.GetComponent<PlayerAbilityQueueSelection>().AbilityQueueItems);

    private NetwPlayerAbilityQueue ConvertToAbilityNetworkList(List<AbilityQueueItem> abilityQueueItems)
        =>
        new NetwPlayerAbilityQueue
        {
            PlayerAbilities = abilityQueueItems
            .Select(abilityQueueItem => ConvertToNetworkAbilItem(abilityQueueItem))
            .ToList()
        };

    private NetwPlayerAbilityQueueItem ConvertToNetworkAbilItem(AbilityQueueItem abilityQueueItem)
    {
        var hexTile2V3 = abilityQueueItem.Hex2 != null ? (Vector3)abilityQueueItem.Hex2.HexCoordinates : Utils.DefaultEmptyV3;

        return new NetwPlayerAbilityQueueItem
        {
            PlayerId = abilityQueueItem.Player.Id,
            HexCoordinates = abilityQueueItem.Hex.HexCoordinates,
            HexCoordinates2 = hexTile2V3,
            Ability = abilityQueueItem.AbilityType,
            Id = abilityQueueItem.Id,
        };
    }

    private List<AbilityQueueItem> ConvertToConcreteList(NetwPlayerAbilityQueue netwPlayerAbilityQueue)
    { 
        return netwPlayerAbilityQueue.PlayerAbilities
        .Select(netwQueueItem => ConvertToAbilQueueItem(netwQueueItem))
        .ToList();
    }

    private AbilityQueueItem ConvertToAbilQueueItem(NetwPlayerAbilityQueueItem netwQueueItem)
    {
        var hex2Tile = netwQueueItem.HexCoordinates2.IsDefaultEmptyVector() ? null : netwQueueItem.HexCoordinates2.GetHex();

        return new AbilityQueueItem(
            netwQueueItem.PlayerId.GetPlayer(),
            netwQueueItem.HexCoordinates.GetHex(),
            hex2Tile,
            netwQueueItem.Ability,
            netwQueueItem.Id);
    }
}

[Serializable]
public class NetwPlayerAbilityQueue
{
    public List<NetwPlayerAbilityQueueItem> PlayerAbilities;
}

[Serializable]
public class NetwPlayerAbilityQueueItem
{
    public int Id;
    public int PlayerId;
    public Vector3 HexCoordinates;
    public Vector3 HexCoordinates2;
    public AbilityType Ability;    
}