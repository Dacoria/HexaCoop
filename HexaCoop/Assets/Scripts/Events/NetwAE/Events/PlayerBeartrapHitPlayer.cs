using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class NetworkAE : MonoBehaviour
{
    public void PlayerBeartrapHitPlayer(PlayerScript playerWhoOwnsTrap, Hex hexTile, PlayerScript hitByTrap)
    {
        photonView.RPC("RPC_AE_PlayerBeartrapHitPlayer", RpcTarget.All, playerWhoOwnsTrap.Id, (Vector3)hexTile.HexCoordinates, hitByTrap.Id);
    }

    [PunRPC]
    public void RPC_AE_PlayerBeartrapHitPlayer(int pIdWhoShotRocket, Vector3 hexTile, int pIdhitByRocket)
    {
        ActionEvents.PlayerBeartrapHitPlayer?.Invoke(pIdWhoShotRocket.GetPlayer(), hexTile.GetHex(), pIdhitByRocket.GetPlayer());
    }
}