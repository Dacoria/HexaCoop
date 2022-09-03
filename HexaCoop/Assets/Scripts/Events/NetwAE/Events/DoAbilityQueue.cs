using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public partial class NetworkAE : MonoBehaviour
{ 
    public void StartAbilityQueue(List<AbilityQueueItem> AbilityQueueItems)
    {
        var netwQueue = ConvertToAbilityNetworkList(AbilityQueueItems);
        var abilitiesQueueJson = JsonUtility.ToJson(netwQueue);
        photonView.RPC("RPC_AE_StartAbilityQueue", RpcTarget.All, abilitiesQueueJson);
    }

    [PunRPC]
    public void RPC_AE_StartAbilityQueue(string abilitiesJson)
    {
        var netwPlayerAbilityQueue = JsonUtility.FromJson<NetwPlayerAbilityQueue>(abilitiesJson);
        var playerAbilityQueue = ConvertToConcreteList(netwPlayerAbilityQueue);
        ActionEvents.StartAbilityQueue?.Invoke(playerAbilityQueue);
    }
}