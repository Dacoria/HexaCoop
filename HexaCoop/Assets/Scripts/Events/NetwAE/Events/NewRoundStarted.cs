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
        if (currentPlayer == null && !Settings.UseSimultaniousTurns) { throw new System.Exception("null = eigen player bij sim turns. dit hoort niet"); }
        photonView.RPC("RPC_AE_NewRoundStarted", RpcTarget.All, players.Select(x => x.Id).ToArray(), currentPlayer?.Id);
    }

    [PunRPC]
    public void RPC_AE_NewRoundStarted(int[] playerIds, int currentPlayerId)
    {
        if (currentPlayerId == -1 && !Settings.UseSimultaniousTurns) { throw new System.Exception("-1 = eigen player bij sim turns. dit hoort niet"); }
        ActionEvents.NewRoundStarted?.Invoke(playerIds.Select(x => x.GetPlayer()).ToList(), currentPlayerId == -1 ? Netw.MyPlayer() : currentPlayerId.GetPlayer());
    }
}