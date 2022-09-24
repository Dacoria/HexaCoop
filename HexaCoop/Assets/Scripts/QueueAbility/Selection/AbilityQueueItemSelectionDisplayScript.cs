using System;
using UnityEngine;
using UnityEngine.UI;

public class AbilityQueueItemSelectionDisplayScript : MonoBehaviour
{
    [SerializeField] private Image AbilityImage;
    [SerializeField] private Image BackgroundAbility;
    [SerializeField] private Image DirectionImage;

    [HideInInspector][ComponentInject] public Button Button;

    private void Awake()
    {
        this.ComponentInject();
    }

    public void SetAbility(AbilityQueueItem abilityQueueItem)
    {
        AbilityImage.sprite = Rsc.SpriteMap.Get(abilityQueueItem.AbilityType.ToString());
        BackgroundAbility.color = abilityQueueItem.Player.Color;

        MonoHelper.instance.SetSpriteDirectionOnImage(DirectionImage, abilityQueueItem.AbilityType, abilityQueueItem.Player.CurrentHexTile.HexCoordinates, abilityQueueItem.Hex.HexCoordinates);
    }
}