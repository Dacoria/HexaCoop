using System.Collections;
using UnityEngine;

public class HideShowMobile: MonoBehaviour
{    
    [ComponentInject] private CanvasGroup canvasGroup;

    public bool ShowOnMobile = false;
    public bool AlwaysShow = false;

    private void Start()
    {
        this.ComponentInject();
        MobileShower.ToggleMobile += OnToggleMobile;

        MobileShower.IsShowingMobile = Application.isMobilePlatform;
        UpdateMobShowStatus();
    }

    private void OnToggleMobile()
    {
        UpdateMobShowStatus();
    }

    private void UpdateMobShowStatus()
    {
        var show = ShowOnMobile ? MobileShower.IsShowingMobile : !MobileShower.IsShowingMobile;
        if(AlwaysShow)
        {
            show = true;
        }

        canvasGroup.alpha = show ? 1 : 0;
        canvasGroup.interactable = show ? false : true;
        canvasGroup.blocksRaycasts = show ? true : false;
    }

    private void OnDestroy()
    {
        MobileShower.ToggleMobile -= OnToggleMobile;
    }
}
