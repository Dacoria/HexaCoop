﻿using Photon.Pun;
using UnityEngine;

public partial class NetworkActionEvents : MonoBehaviour
{    
    public void EnemyFaseStarted()
    {
        photonView.RPC("RPC_AE_EnemyFaseStarted", RpcTarget.All);
    }

    [PunRPC]
    public void RPC_AE_EnemyFaseStarted()
    {
        ActionEvents.EnemyFaseStarted?.Invoke();
    }
}