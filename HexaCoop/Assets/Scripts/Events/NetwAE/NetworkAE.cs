using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class NetworkAE : MonoBehaviour
{
    [ComponentInject] private PhotonView photonView;
    public static NetworkAE instance;

    private void Awake()
    {
        instance = this;
        this.ComponentInject();
    }
    
    // REST WORDT INGELADEN VIA PARTIAL CLASSES
}