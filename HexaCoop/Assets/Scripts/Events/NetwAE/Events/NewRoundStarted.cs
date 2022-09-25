using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class NetworkAE : MonoBehaviour
{
    public void NewRoundStarted_Simultanious(List<PlayerScript> players)
    {
        photonView.RPC("RPC_AE_NewRoundStarted", RpcTarget.All, players.Select(x => x.Id).ToArray(), -1);
    }

    public void NewRoundStarted_Sequential(List<PlayerScript> players, PlayerScript currentPlayer)
    {
        photonView.RPC("RPC_AE_NewRoundStarted", RpcTarget.All, players.Select(x => x.Id).ToArray(), currentPlayer?.Id);
    }

    [PunRPC]
    public void RPC_AE_NewRoundStarted(int[] playerIds, int currentPlayerId)
    {
        ActionEvents.NewRoundStarted?.Invoke(playerIds.Select(x => x.GetPlayer()).ToList(), currentPlayerId == -1 ? Netw.MyPlayer() : currentPlayerId.GetPlayer());
    }
}