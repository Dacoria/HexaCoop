using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HideGoOnPunClient : MonoBehaviour
{
    public bool AlsoHideForPunMaster;

    void Update()
    {
        var canvasGroup = GetComponent<CanvasGroup>();

        if (AlsoHideForPunMaster)
        {
            canvasGroup.alpha = 0;
        }
        else
        {
            canvasGroup.alpha = PhotonNetwork.IsMasterClient && Settings.ShowPunMcButtons ? 1 : 0;
        }     
    }
}
