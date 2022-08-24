using Photon.Pun;
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

        canvasGroup.interactable = canvasGroup.alpha == 1 ? true : false;
    }
}