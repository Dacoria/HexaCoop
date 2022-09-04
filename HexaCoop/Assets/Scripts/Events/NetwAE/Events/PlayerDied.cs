using Photon.Pun;
using UnityEngine;

public partial class NetworkAE : MonoBehaviour
{ 
    public void PlayerDied(PlayerScript player)
    {
        photonView.RPC("RPC_AE_PlayerDied", RpcTarget.All, player.Id);
    }

    [PunRPC]
    public void RPC_AE_PlayerDied(int currentPlayerId)
    {
        ActionEvents.PlayerDied?.Invoke(currentPlayerId == -1 ? Netw.MyPlayer() : currentPlayerId.GetPlayer());
    }
}