using Photon.Pun;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public partial class NetworkAE : MonoBehaviour
{ 
    public void Invoker_EndPlayerTurnWithQueue(PlayerScript currentPlayer)
    {
        var netwQueue = GetQueueOfPlayer();
        var netwQueueJson = JsonUtility.ToJson(netwQueue);
        photonView.RPC("RPC_AE_EndPlayerTurnWithQueue", RpcTarget.All, currentPlayer.Id, netwQueueJson);
    }

    [PunRPC]
    public void RPC_AE_EndPlayerTurnWithQueue(int currentPlayerId, string abilitiesJson)
    {
        var netwPlayerAbilityQueue = JsonUtility.FromJson<NetwPlayerAbilityQueue>(abilitiesJson);
        ActionEvents.EndPlayerTurnWithQueue?.Invoke(currentPlayerId.GetPlayer(), ConvertToConcreteList(netwPlayerAbilityQueue));
    }

    private NetwPlayerAbilityQueue GetQueueOfPlayer()
        =>
        new NetwPlayerAbilityQueue {
            PlayerAbilities = AbilitiesQueueScript.instance.AbilityQueueItems
            .Select(
                abilityQueueItem => new NetwPlayerAbilityQueueItem
                {
                    PlayerId = abilityQueueItem.Player.Id,
                    HexCoordinates = abilityQueueItem.Hex.HexCoordinates,
                    Ability = abilityQueueItem.AbilityType
                }
            ).ToList()
        };

    private List<AbilityQueueItem> ConvertToConcreteList(NetwPlayerAbilityQueue netwPlayerAbilityQueue)
        => netwPlayerAbilityQueue.PlayerAbilities
        .Select(netwQueueItem => new AbilityQueueItem(netwQueueItem.PlayerId.GetPlayer(), netwQueueItem.HexCoordinates.GetHex(), netwQueueItem.Ability))
        .ToList();
}

[Serializable]
public class NetwPlayerAbilityQueue
{
    public List<NetwPlayerAbilityQueueItem> PlayerAbilities;
}

[Serializable]
public class NetwPlayerAbilityQueueItem
{
    public int PlayerId;
    public Vector3 HexCoordinates;
    public AbilityType Ability;
}