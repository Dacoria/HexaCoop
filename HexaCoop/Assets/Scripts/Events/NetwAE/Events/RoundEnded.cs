using Photon.Pun;
using UnityEngine;

public partial class NetworkAE : MonoBehaviour
{
    public void RoundEnded(bool achievedTarget, PlayerScript pWinner)
    {
        photonView.RPC("RPC_AE_RoundEnded", RpcTarget.All, achievedTarget, pWinner.Id);
    }

    [PunRPC]
    public void RPC_AE_RoundEnded(bool achievedTarget, int pWinnerId)
    {
        ActionEvents.EndRound?.Invoke(achievedTarget, pWinnerId.GetPlayer());
    }
}