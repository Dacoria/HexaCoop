using Photon.Pun;
using UnityEngine;

public partial class NetworkAE : MonoBehaviour
{ 
    public void NewPlayerTurn(PlayerScript currentPlayer)
    {
        photonView.RPC("RPC_AE_NewPlayerTurn", RpcTarget.All, currentPlayer == null ? -1 : currentPlayer.Id);
    }

    [PunRPC]
    public void RPC_AE_NewPlayerTurn(int currentPlayerId)
    {
        ActionEvents.NewPlayerTurn?.Invoke(currentPlayerId == -1 ? null : currentPlayerId.GetPlayer());
    }
}