using System;
using UnityEngine;

public class HideShowMobile: MonoBehaviour
{
    [ComponentInject] private CanvasGroup canvasGroup;

    public bool ShowOnMobile = true;

    private void Start()
    {
        this.ComponentInject();
        MobileShower.ToggleMobile += OnToggleMobile;
        UpdateMobShowStatus();
    }

    private void OnToggleMobile()
    {
        UpdateMobShowStatus();
    }

    private void UpdateMobShowStatus()
    {
        var show = ShowOnMobile ? MobileShower.IsShowingMobile : !MobileShower.IsShowingMobile;

        canvasGroup.alpha = show ? 1 : 0;
        canvasGroup.interactable = show ? false : true;
        canvasGroup.blocksRaycasts = show ? true : false;
    }

    private void OnDestroy()
    {
        MobileShower.ToggleMobile -= OnToggleMobile;
    }
}
