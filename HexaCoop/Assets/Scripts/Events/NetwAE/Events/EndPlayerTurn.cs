using Photon.Pun;
using UnityEngine;

public partial class NetworkAE : MonoBehaviour
{ 
    public void Invoker_EndPlayerTurn(PlayerScript currentPlayer)
    {
        photonView.RPC("RPC_AE_EndPlayerTurn", RpcTarget.All, currentPlayer.Id);
    }

    [PunRPC]
    public void RPC_AE_EndPlayerTurn(int currentPlayerId)
    {
        ActionEvents.EndPlayerTurn?.Invoke(currentPlayerId.GetPlayer());
    }
}