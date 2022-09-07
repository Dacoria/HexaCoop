using System;
using UnityEngine;

public class TooltipUIHandler : HexaEventCallback
{
    private bool toolTipIsActive;
    private Outline OutlineComponent;

    [ComponentInject] private ITooltipUIText CallingTooltipBahaviour;

    public void OnDestroy()
    {
        if (toolTipIsActive)
        {
            RemoveTooltip();
        }
    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))            
        {
            LeftClick();
        }
    }

    private void LeftClick()
    {
        if(!ButtonUpdater.instance.AbilitySelectionInProgress &&
            !toolTipIsActive 
            && (DateTime.Now - TimeTooltipRemoved).TotalMilliseconds > 100)
        {
            var header = CallingTooltipBahaviour.GetHeaderText();
            var content = CallingTooltipBahaviour.GetContentText();
            TooltipSystem.instance.Show(content, header, waitBeforeShowing: false, activeTooltipGo: gameObject);

            toolTipIsActive = true;
            SetOutline();

            TimeTooltipShown = DateTime.Now;
        }
    }

    private void SetOutline()
    {
        OutlineComponent = gameObject.GetAdd<Outline>();
        OutlineComponent.OutlineColor = new Color(23 / 255f, 171 / 255f, 178 / 255f);
        OutlineComponent.OutlineWidth = 6.5f;
    }

    private DateTime TimeTooltipRemoved;
    private DateTime TimeTooltipShown;

    private void RemoveTooltip()
    {
        if(OutlineComponent != null)
        {
            Destroy(OutlineComponent);
        }
        TooltipSystem.instance.Hide();
        toolTipIsActive = false;
        TimeTooltipRemoved = DateTime.Now;
    }

    public void Update()
    {
        if (!toolTipIsActive)
        {
            return;
        }

        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) 
            && (DateTime.Now - TimeTooltipShown).TotalMilliseconds > 20)
        {
            RemoveTooltip();            
        }
    } 
}