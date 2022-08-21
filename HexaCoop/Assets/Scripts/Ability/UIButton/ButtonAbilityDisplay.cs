using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ButtonAbilityDisplay : MonoBehaviour
{
    public AbilityType Type;
    [ComponentInject] private TMP_Text CostText;
    [ComponentInject] private Image ImageButton;

    [ComponentInject] public Button Button;// public, zodat andere klasses er makkelijk bij kunnen

    public ButtonUpdater buttonUpdater;

    public Image ImageAbility;
    private Sprite buttonNotPressed;

    private void Awake()
    {
        this.ComponentInject();
        ImageAbility = this.GetComponentOnlyInDirectChildren<Image>();
        this.buttonNotPressed = ImageButton.sprite;
    }

    private void Start()
    {
        CostText.text = Type.Cost().ToString();        
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
            ImageButton.sprite = _abilityIsActive ? buttonUpdater.ButtonPressedSprite : buttonNotPressed;
        }
    }

    private void Update()
    {
        if(!Button.interactable)
        {
            ImageButton.color = DisabledColor;
        }
        else 
        {
            ImageButton.color = AbilityIsActive ? SelectedColor : UnselectedColor;
        }
    }

    public void OnButtonClick()
    {
        buttonUpdater.OnAbilityButtonClick(this); // disabled alle buttons        
    }   
}
