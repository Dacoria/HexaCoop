using Photon.Pun;
using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public partial class NetworkAE : MonoBehaviour
{ 
    public void EndPlayerTurn(PlayerScript currentPlayer)
    {
        string abilitiesQueueJson = null;
        if(Settings.UseQueueAbilities)
        {
            var netwQueue = GetQueueOfPlayer();
            abilitiesQueueJson = JsonUtility.ToJson(netwQueue);
        }
        
        photonView.RPC("RPC_AE_EndPlayerTurn", RpcTarget.All, currentPlayer.Id, abilitiesQueueJson);
    }

    [PunRPC]
    public void RPC_AE_EndPlayerTurn(int currentPlayerId, string abilitiesJson)
    {
        List<AbilityQueueItem> playerAbilityQueue = null;
        if (Settings.UseQueueAbilities)
        {
            var netwPlayerAbilityQueue = JsonUtility.FromJson<NetwPlayerAbilityQueue>(abilitiesJson);
            playerAbilityQueue = ConvertToConcreteList(netwPlayerAbilityQueue);
        }
        
        ActionEvents.EndPlayerTurn?.Invoke(currentPlayerId.GetPlayer(), playerAbilityQueue);
    }

    private NetwPlayerAbilityQueue GetQueueOfPlayer() => ConvertToAbilityNetworkList(AbilitiesQueueScript.instance.AbilityQueueItems);

    private NetwPlayerAbilityQueue ConvertToAbilityNetworkList(List<AbilityQueueItem> abilityQueueItems)
        =>
        new NetwPlayerAbilityQueue
        {
            PlayerAbilities = abilityQueueItems.Select(
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