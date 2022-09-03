using Photon.Pun;
using UnityEngine;

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