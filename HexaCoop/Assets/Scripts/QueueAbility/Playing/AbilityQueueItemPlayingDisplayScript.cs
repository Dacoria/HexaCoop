using UnityEngine;
using UnityEngine.UI;

public class AbilityQueueItemPlayingDisplayScript : HexaEventCallback
{
    [SerializeField] private Image AbilityImage;
    [SerializeField] private Image BackgroundAbility;
    [SerializeField] private Image ActiveAbilityHighlighter;
    [SerializeField] private Image NotExecutedCross;
    [ComponentInject] private CanvasGroup canvasGroup;
    
    public int QueueItemId;

    public void SetAbility(int id, AbilityType abilityType, Color colorBg)
    {
        QueueItemId = id;
        AbilityImage.sprite = Rsc.SpriteMap.Get(abilityType.ToString());
        BackgroundAbility.color = colorBg;

        SetIsActiveAbility(false);
        NotExecutedCross.gameObject.SetActive(false);
    }

    private bool isActivated;

    public void SetIsActiveAbility(bool isActive)
    {
        if(isActive)
        {
            isActivated = true;
        }


        if(isActivated && !isActive)
        {
            // van actief naar niet actief --> iets vervagen
            canvasGroup.alpha = 0.6f;
        }

        ActiveAbilityHighlighter.gameObject.SetActive(isActive);
    }

    public void SetIsNotExecuted()
    {
        NotExecutedCross.gameObject.SetActive(true);
    }
}
