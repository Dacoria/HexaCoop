using System;
using UnityEngine;
using UnityEngine.UI;

public class AbilityQueueItemPlayingDisplayScript : HexaEventCallback
{
    [SerializeField] private Image AbilityImage;
    [SerializeField] private Image BackgroundAbility;
    [SerializeField] private Image ActiveAbilityHighlighter;
    [SerializeField] private Image NotExecutedCross;

    [SerializeField] private Image DirectionImage;

    [ComponentInject] private CanvasGroup canvasGroup;
    
    public int QueueItemId;


    private bool isActivated;

    public void SetAbility(AbilityQueueItem abilityQueueItem)
    {
        QueueItemId = abilityQueueItem.Id;
        AbilityImage.sprite = Rsc.SpriteMap.Get(abilityQueueItem.AbilityType.ToString());
        BackgroundAbility.color = abilityQueueItem.Player.Color;

        MonoHelper.instance.SetSpriteDirectionOnImage(DirectionImage, abilityQueueItem.AbilityType, abilityQueueItem.Player.CurrentHexTile.HexCoordinates, abilityQueueItem.Hex.HexCoordinates);

        SetIsActiveAbility(false);
        NotExecutedCross.gameObject.SetActive(false);
    }

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
