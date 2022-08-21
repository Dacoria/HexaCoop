using Photon.Pun;
using UnityEngine;

public partial class NetworkAE : MonoBehaviour
{
    public void AllPlayersFinishedTurn()
    {
        photonView.RPC("RPC_AE_AllPlayersFinishedTurn", RpcTarget.All);
    }

    [PunRPC]
    public void RPC_AE_AllPlayersFinishedTurn()
    {
        ActionEvents.AllPlayersFinishedTurn?.Invoke();
    }
}