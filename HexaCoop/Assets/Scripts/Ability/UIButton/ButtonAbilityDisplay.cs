using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ButtonAbilityDisplay : HexaEventCallback
{
    public AbilityType Type;
    [ComponentInject] private TMP_Text costText;
    [ComponentInject] private Image imageButton;
    [ComponentInject] private CanvasGroup canvasGroup;

    [ComponentInject] public Button Button;// public, zodat andere klasses er makkelijk bij kunnen

    public ButtonUpdater buttonUpdater;

    public Image ImageAbility;
    private Sprite buttonNotPressed;

    private new void Awake()
    {
        base.Awake();
        ImageAbility = this.GetComponentOnlyInDirectChildren<Image>();
        this.buttonNotPressed = imageButton.sprite;
    }

    private void Start()
    {
        costText.text = Type.GetCost().ToString();        
    }

    private Color UnselectedColor = new Color(81 / 255f, 216 / 255f, 255 / 255f); // blue (on grey)
    private Color SelectedColor = new Color(83 / 255f, 255 / 255f, 0 / 255f); // green (on grey)
    private Color DisabledColor = new Color(1, 1, 1); // grey

    private bool _abilityIsActive;
    public bool AbilityIsActive 
    { 
        get => _abilityIsActive;
        set
        {
            _abilityIsActive = value;
            imageButton.sprite = _abilityIsActive ? buttonUpdater.ButtonPressedSprite : buttonNotPressed;
        }
    }

    protected override void OnNewPlayerTurn(PlayerScript player)
    {
        //RefreshPickupAbility
        var pickupsOfPlayer = player.GetComponent<PlayerAbilityPickups>();
        hasPickupAbility = pickupsOfPlayer.HasPickupAbility(Type);
    }

    private bool hasPickupAbility;

    private void Update()
    {
        if(!hasPickupAbility && Type.IsPickup() && !Button.interactable)
        {
            canvasGroup.alpha = 0;
            return;
        }

        canvasGroup.alpha = 1;

        if (!Button.interactable)
        {
            imageButton.color = DisabledColor;
        }
        else 
        {
            imageButton.color = AbilityIsActive ? SelectedColor : UnselectedColor;
        }
    }

    public void OnButtonClick()
    {
        buttonUpdater.OnAbilityButtonClick(this); // disabled alle buttons        
    }   
}
