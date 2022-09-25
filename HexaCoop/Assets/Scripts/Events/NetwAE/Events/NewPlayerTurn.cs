using Photon.Pun;
using System;
using System.Linq;
using UnityEngine;

public partial class NetworkAE : MonoBehaviour
{ 
    public void NewPlayerTurn_Simultanious()
    {
        photonView.RPC("RPC_AE_NewPlayerTurn", RpcTarget.All, -1);
    }

    public void NewPlayerTurn_Sequential(PlayerScript currentPlayer)
    {
        photonView.RPC("RPC_AE_NewPlayerTurn", RpcTarget.All, currentPlayer.Id);
    }

    [PunRPC]
    public void RPC_AE_NewPlayerTurn(int currentPlayerId)
    {
        // -1 betekent: Eigen netwerk speler
        if(OwnPlayerIsDeadInSimTurns(currentPlayerId))
        {
            TrySetTurnEventToAiOnNetwork();
            return;
        }

        ActionEvents.NewPlayerTurn?.Invoke(currentPlayerId == -1 ? Netw.MyPlayer() : currentPlayerId.GetPlayer());
    }
    

    private bool OwnPlayerIsDeadInSimTurns(int currentPlayerId)
    {
        return currentPlayerId == -1 && !Netw.MyPlayer().IsAlive;
    }

    private void TrySetTurnEventToAiOnNetwork()
    {
        var aisOnMyNetwork = Netw.PlayersOnMyNetwork(isAlive: true, isAi: true);
        if(aisOnMyNetwork.Any())
        {
            ActionEvents.NewPlayerTurn?.Invoke(aisOnMyNetwork.First());
        }
    }
}