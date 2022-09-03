using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TakeMcControlScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool IsHoldingMouseClick;
    private float timeHoldingMouse;

    private void Awake()
    {
        this.ComponentInject();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsHoldingMouseClick = true;
        Settings.ShowPunMcButtons = false;
    }

    public void OnPointerUp(PointerEventData eventData) => IsHoldingMouseClick = false;

    private void Update()
    {
        if(!IsHoldingMouseClick)
        {
            timeHoldingMouse = 0;
        }        
        else
        {
            timeHoldingMouse += Time.deltaTime;

            if (timeHoldingMouse >= 1f)
            {
                Settings.ShowPunMcButtons = true;
                if (!PhotonNetwork.IsMasterClient)
                {
                    PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                }
            }
        }
    }
}