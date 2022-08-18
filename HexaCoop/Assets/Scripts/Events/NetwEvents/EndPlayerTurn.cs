using Photon.Pun;
using UnityEngine;

public partial class NetworkActionEvents : MonoBehaviour
{ 
    public void EndPlayerTurn(PlayerScript currentPlayer)
    {
        photonView.RPC("RPC_AE_EndPlayerTurn", RpcTarget.All, currentPlayer.PlayerId);
    }

    [PunRPC]
    public void RPC_AE_EndPlayerTurn(int currentPlayerId)
    {
        ActionEvents.EndPlayerTurn?.Invoke(currentPlayerId.GetPlayer());
    }
}