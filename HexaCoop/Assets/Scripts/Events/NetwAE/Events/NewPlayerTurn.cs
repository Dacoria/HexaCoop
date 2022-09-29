using Photon.Pun;
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
        var activeAIOnNetworkWithoutTurn = GetActiveAIOnNetworkWithoutTurn();
        if (OwnPlayerIsDeadInSimTurns(currentPlayerId) && activeAIOnNetworkWithoutTurn != null)
        {
            ActionEvents.NewPlayerTurn?.Invoke(activeAIOnNetworkWithoutTurn);
        }
        else
        {
            ActionEvents.NewPlayerTurn?.Invoke(currentPlayerId == -1 ? Netw.MyPlayer() : currentPlayerId.GetPlayer());
        }
    }
    

    private bool OwnPlayerIsDeadInSimTurns(int currentPlayerId)
    {
        return currentPlayerId == -1 && !Netw.MyPlayer().IsAlive;
    }

    private PlayerScript GetActiveAIOnNetworkWithoutTurn()
    {
        var aisOnMyNetworkWithoutTurn = Netw.PlayersOnMyNetwork(isAlive: true, isAi: true).Where(x => x.TurnCount < GameHandler.instance.CurrentTurn);
        return aisOnMyNetworkWithoutTurn.FirstOrDefault();
    }
}