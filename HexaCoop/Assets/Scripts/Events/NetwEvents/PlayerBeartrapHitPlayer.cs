using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class NetworkActionEvents : MonoBehaviour
{
    public void PlayerBeartrapHitPlayer(PlayerScript playerWhoOwnsTrap, Hex hexTile, PlayerScript hitByTrap)
    {
        photonView.RPC("RPC_AE_PlayerBeartrapHitPlayer", RpcTarget.All, playerWhoOwnsTrap.PlayerId, (Vector3)hexTile.HexCoordinates, hitByTrap.PlayerId);
    }

    [PunRPC]
    public void RPC_AE_PlayerBeartrapHitPlayer(int pIdWhoShotRocket, Vector3 hexTile, int pIdhitByRocket)
    {
        ActionEvents.PlayerBeartrapHitPlayer?.Invoke(pIdWhoShotRocket.GetPlayer(), hexTile.GetHex(), pIdhitByRocket.GetPlayer());
    }
}