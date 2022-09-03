using Photon.Pun;
using UnityEngine;

public partial class NetworkAE : MonoBehaviour
{
    public void EndGame()
    {
        photonView.RPC("RPC_AE_EndGame", RpcTarget.All);
    }

    [PunRPC]
    public void RPC_AE_EndGame()
    {
        ActionEvents.EndGame?.Invoke();
    }
}