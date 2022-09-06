using System;
using System.Collections;
using UnityEngine;

public class TooltipSystem : MonoBehaviour
{
    public static TooltipSystem instance;
    public Tooltip Tooltip;

    [SerializeField]
    private float waitTimeToShow = 0.5f;
    [SerializeField]
    private float fadeInTime = 0.5f;
    [SerializeField]
    private float fadeOutTime = 0.5f;

    public void Awake()
    {
        instance = this;
    }    

    private Color ColorBackground;
    private Color ColorHeader;
    private Color ColorContent;

    void Start()
    {
        Tooltip.gameObject.SetActive(true);
        ColorBackground = Tooltip.BackGround.color;
        ColorHeader = Tooltip.HeaderField.color;
        ColorContent = Tooltip.ContentField.color;

        // start invisible
        SetTooltipAlphaColor(0);
    }    

    private DateTime showTimeTooltip;

    public void Show(string content = "", string header = "", bool waitBeforeShowing = true, GameObject activeTooltipGo = null)
    {
        ActiveTooltipGo = activeTooltipGo;
        StopAllCoroutines();
        instance.Tooltip.SetText(content, header);
        showTimeTooltip = DateTime.Now;        

        if (waitBeforeShowing)
        {
            instance.StartCoroutine(instance.ShowAfterXSeconds());
        }
        else
        {
            instance.StartCoroutine(instance.FadeIn());
        }
    }

    private GameObject ActiveTooltipGo;

    public bool UpdateText(GameObject activeTooltipGo, string content, string header = "")
    {
        // voorkomt dat 2 updates tegelijkertijd bezig zijn (wat kan via een event + rayhit)
        if(activeTooltipGo == ActiveTooltipGo)
        {
            instance.Tooltip.SetText(content, header);
            return true;
        }
        return false;
    }

    public IEnumerator ShowAfterXSeconds()
    {
        yield return Wait4Seconds.Get(waitTimeToShow);
        instance.StartCoroutine(instance.FadeIn());
    }

    public void Hide(bool ignoreWaitBuffer = false)
    {
        // stel je klikt van 1 tooltip op een persoon naar de volgende --> dan moet de tooltip blijven (uitzondering: hover)
        if (ignoreWaitBuffer || (DateTime.Now - showTimeTooltip).TotalMilliseconds > 50)
        {
            StopAllCoroutines();
            instance.StartCoroutine(instance.FadeOut());
        }
    }

    private IEnumerator FadeOut()
    {
        var initialA = (Tooltip.BackGround.color.a * fadeOutTime);
        for (float a = initialA; a >= 0; a = a - Time.deltaTime)
        {
            SetTooltipAlphaColor(a / fadeOutTime);
            yield return null;
        }
        SetTooltipAlphaColor(0);
    }

    private IEnumerator FadeIn()
    {
        var initialA = Tooltip.BackGround.color.a;
        for (float a = 0; a <= fadeInTime; a += Time.deltaTime)
        {
            SetTooltipAlphaColor(Math.Min(1, initialA + (a / fadeInTime)));
            yield return null;
        }
    }

    private void SetTooltipAlphaColor(float alphaColorValue)
    {
        Tooltip.BackGround.color = new Color(ColorBackground.r, ColorBackground.g, ColorBackground.b, alphaColorValue);
        Tooltip.HeaderField.color = new Color(ColorHeader.r, ColorHeader.g, ColorHeader.b, alphaColorValue);
        Tooltip.ContentField.color = new Color(ColorContent.r, ColorContent.g, ColorContent.b, alphaColorValue);
    }
}
