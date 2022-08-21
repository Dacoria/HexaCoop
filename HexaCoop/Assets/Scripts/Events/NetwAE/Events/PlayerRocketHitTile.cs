using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class NetworkAE : MonoBehaviour
{    
    public void PlayerDamageObjectHitTile(PlayerScript playerWhoShotRocket, Hex hexTile, DamageObjectType doType)
    {
        photonView.RPC("RPC_AE_PlayerDamageObjectHitTile", RpcTarget.All, playerWhoShotRocket.Id, (Vector3)hexTile.HexCoordinates, doType);
    }

    [PunRPC]
    public void RPC_AE_PlayerDamageObjectHitTile(int pIdWhoShotRocket, Vector3 hexTile, DamageObjectType doType)
    {
        ActionEvents.PlayerDamageObjectHitTile?.Invoke(pIdWhoShotRocket.GetPlayer(), hexTile.GetHex(), doType);
    }
}