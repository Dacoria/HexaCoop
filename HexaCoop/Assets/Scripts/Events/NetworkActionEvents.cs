using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public partial class NetworkActionEvents : MonoBehaviour
{
    [ComponentInject] private PhotonView photonView;
    public static NetworkActionEvents instance;

    private void Awake()
    {
        instance = this;
        this.ComponentInject();
    }
    
    // REST WORDT INGELADEN VIA PARTIAL CLASSES
}