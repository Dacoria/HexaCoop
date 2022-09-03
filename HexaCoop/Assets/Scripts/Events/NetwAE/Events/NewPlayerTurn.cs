using Photon.Pun;
using UnityEngine;

public partial class NetworkAE : MonoBehaviour
{ 
    public void NewPlayerTurn_Simultanious()
    {
        photonView.RPC("RPC_AE_NewPlayerTurn", RpcTarget.All, -1);
    }

    public void NewPlayerTurn_Sequential(PlayerScript currentPlayer = null)
    {
        if (currentPlayer == null && !Settings.UseSimultaniousTurns) { throw new System.Exception("null = eigen player bij sim turns. dit hoort niet"); }
        photonView.RPC("RPC_AE_NewPlayerTurn", RpcTarget.All, currentPlayer.Id);
    }

    [PunRPC]
    public void RPC_AE_NewPlayerTurn(int currentPlayerId)
    {
        if (currentPlayerId == -1 && !Settings.UseSimultaniousTurns) { throw new System.Exception("-1 = eigen player bij sim turns. dit hoort niet"); }
        ActionEvents.NewPlayerTurn?.Invoke(currentPlayerId == -1 ? Netw.MyPlayer() : currentPlayerId.GetPlayer());
    }
}