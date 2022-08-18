using Photon.Pun;
using UnityEngine;

public partial class NetworkActionEvents : MonoBehaviour
{
    public void RoundEnded(bool achievedTarget, PlayerScript pWinner)
    {
        photonView.RPC("RPC_AE_RoundEnded", RpcTarget.All, achievedTarget, pWinner.PlayerId);
    }

    [PunRPC]
    public void RPC_AE_RoundEnded(bool achievedTarget, int pWinnerId)
    {
        ActionEvents.EndRound?.Invoke(achievedTarget, pWinnerId.GetPlayer());
    }
}