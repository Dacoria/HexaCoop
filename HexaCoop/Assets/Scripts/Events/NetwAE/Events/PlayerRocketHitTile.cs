﻿using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class NetworkAE : MonoBehaviour
{    
    public void PlayerRocketHitTile(PlayerScript playerWhoShotRocket, Hex hexTile)
    {
        photonView.RPC("RPC_AE_PlayerRocketHitTile", RpcTarget.All, playerWhoShotRocket.Id, (Vector3)hexTile.HexCoordinates);
    }

    [PunRPC]
    public void RPC_AE_PlayerRocketHitTile(int pIdWhoShotRocket, Vector3 hexTile)
    {
        ActionEvents.PlayerRocketHitTile?.Invoke(pIdWhoShotRocket.GetPlayer(), hexTile.GetHex());
    }
}