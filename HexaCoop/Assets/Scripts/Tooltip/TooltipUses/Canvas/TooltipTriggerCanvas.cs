using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipTriggerCanvas : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public string Header;
    public string Content;

    private Vector2 PositionMouseOnStartTooltip;

    private bool activeTooltip;

    public void Start ()
    {
        if(TooltipSystem.instance == null)
        {
            Destroy(this);
        }
    }

    public void Update()
    {        
        if(Input.GetMouseButtonDown(1))
        {
            if (!activeTooltip)
            {
                PositionMouseOnStartTooltip = Input.mousePosition;
                TooltipSystem.instance.Show(Content, Header);
                activeTooltip = true;
            }
            else
            {
                StopTooltip();
            }
        }

        if(!activeTooltip)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            StopTooltip();
        }

        if(Vector2.Distance(PositionMouseOnStartTooltip, Input.mousePosition) > 50)
        {
            StopTooltip();
        }
    }

    private void StopTooltip()
    {
        TooltipSystem.instance.Hide(ignoreWaitBuffer: true);
        activeTooltip = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StopTooltip();
    }
}
