using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideGoOnPunClient : MonoBehaviour
{
    public bool AlsoHideForPunMaster;

    void Start()
    {
        if(!PhotonNetwork.IsMasterClient || AlsoHideForPunMaster)
        {
            var canvasGroup = GetComponent<CanvasGroup>(); ;
            canvasGroup.alpha = 0;
        }
    }
}
