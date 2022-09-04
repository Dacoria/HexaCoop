using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class NetworkAE : MonoBehaviour
{
    public void NewSimTurnsPlayOrder(List<PlayerScript> players)
    {
        photonView.RPC("RPC_AE_NewSimTurnsPlayOrder", RpcTarget.All, players.Select(x => x.Id).ToArray(), -1);
    }  

    [PunRPC]
    public void RPC_AE_NewSimTurnsPlayOrder(int[] playerIds, int currentPlayerId)
    {
        ActionEvents.NewSimTurnsPlayOrder?.Invoke(playerIds.Select(x => x.GetPlayer()).ToList());
    }
}