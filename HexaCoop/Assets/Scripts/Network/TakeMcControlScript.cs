using Photon.Pun;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TakeMcControlScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool IsHoldingMouseClick;
    private float timeHoldingMouse;

    public Image MiddleMouseImage;
    public Sprite OrangeMiddleMouseSprite;
    public Sprite RedMcMiddleMouseSprite;
    public Sprite BlueMcMiddleMouseSprite;

    private void Awake()
    {
        this.ComponentInject();
        MiddleMouseImage.sprite = PhotonNetwork.IsMasterClient ? OrangeMiddleMouseSprite : RedMcMiddleMouseSprite;
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
            MiddleMouseImage.sprite = PhotonNetwork.IsMasterClient ? OrangeMiddleMouseSprite : RedMcMiddleMouseSprite;

            timeHoldingMouse += Time.deltaTime;
            if (timeHoldingMouse >= 0.2f)
            {
                Settings.ShowPunMcButtons = true;

                if (timeHoldingMouse >= 0.5f)
                {
                    if (!PhotonNetwork.IsMasterClient)
                    {
                        PhotonNetwork.SetMasterClient(PhotonNetwork.LocalPlayer);
                    }
                }

                if(PhotonNetwork.IsMasterClient)
                {
                    MiddleMouseImage.sprite = BlueMcMiddleMouseSprite;
                }
            }
        }
    }
}