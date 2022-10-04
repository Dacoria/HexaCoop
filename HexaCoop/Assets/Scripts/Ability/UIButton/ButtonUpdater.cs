using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ButtonUpdater : MonoBehaviour
{
    public Sprite ButtonPressedSprite;

    public List<ButtonAbilityDisplay> abilityScripts;
    public static ButtonUpdater instance;

    public bool AbilitySelectionInProgress;

    private void Awake()
    {
        instance = this;
    }

    public void Init()
    {
        this.ComponentInject();
        abilityScripts = GetComponentsInChildren<ButtonAbilityDisplay>().ToList();
    }
    
    public void SetAbilityInteractable(AbilityType type, bool value)
    {
        var abilityScript = abilityScripts.SingleOrDefault(x => x.Type == type);
        if (abilityScript != null)
        {
            abilityScript.Button.interactable = value;
            if (!value)
            {
                AbilitySelectionInProgress = false;
            }
        }
    }

    public void SetToUnselected(AbilityType type)
    {
        var abilityScript = abilityScripts.SingleOrDefault(x => x.Type == type);
        if (abilityScript != null)
        {
            abilityScript.AbilityIsActive = false;
            abilityScript.GetComponent<IAbilityAction>().DeselectAbility();

            AbilitySelectionInProgress = false;
        }
    }

    public void OnAbilityButtonClick(ButtonAbilityDisplay caller)
    {
        abilityScripts.Where(x => x != caller).ToList().ForEach(x => SetToUnselected(x.Type));
        if (!caller.AbilityIsActive)
        {
            caller.AbilityIsActive = true;
            caller.GetComponent<IAbilityAction>().InitAbilityAction();

            AbilitySelectionInProgress = true;
        }
    }
}