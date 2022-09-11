using System;
using UnityEngine;
using UnityEngine.UI;

public class AbilityQueueItemSelectionDisplayScript : MonoBehaviour
{
    [SerializeField] private Image AbilityImage;
    [SerializeField] private Image BackgroundAbility;

    [HideInInspector][ComponentInject] public Button Button;

    private void Awake()
    {
        this.ComponentInject();
    }

    public void SetAbility(AbilityQueueItem abilityQueueItem)
    {
        AbilityImage.sprite = MonoHelper.instance.GetAbilitySprite(abilityQueueItem.AbilityType, abilityQueueItem.Player.CurrentHexTile.HexCoordinates, abilityQueueItem.Hex.HexCoordinates);
        BackgroundAbility.color = abilityQueueItem.Player.Color;
    }
}