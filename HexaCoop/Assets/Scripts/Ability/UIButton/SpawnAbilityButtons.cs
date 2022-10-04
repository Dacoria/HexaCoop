using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SpawnAbilityButtons : HexaEventCallback
{
    public GameObject AbilityButtonPrefab;
    public ButtonUpdater ButtonUpdater;

    private bool hasSpawnedButtons;

    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript player)
    {
        Init();
    }

    private void Init()
    {
        if(hasSpawnedButtons)
        {
            return;
        }

        foreach(var abilityType in Utils.GetValues<AbilityType>().Where(x => x.IsAvailableInGame()))
        {
            if(!abilityType.IsAvailableInCurrentLevel())
            {
                continue;
            }

            var abilityButton = Instantiate(AbilityButtonPrefab, transform);

            var abilityAction = AddAbilityActionComponent(abilityButton, abilityType);

            var buttonAbilityDisplay = abilityButton.AddComponent<ButtonAbilityDisplay>();
            buttonAbilityDisplay.Type = abilityType;
            buttonAbilityDisplay.ImageAbility.sprite = Rsc.SpriteMap.Get(abilityType.ToString());
            buttonAbilityDisplay.buttonUpdater = ButtonUpdater;
            buttonAbilityDisplay.GetComponent<Button>().onClick.AddListener(() => buttonAbilityDisplay.OnButtonClick());
        }

        ButtonUpdater.Init();
        hasSpawnedButtons = true;
    }

    private IAbilityAction AddAbilityActionComponent(GameObject abilityButton, AbilityType abilityType)
    {
        // zit ability type in de naam??? dat is vereist
        var abilityDisplayScript = TypeUtil.GetTypesAssignableFrom(typeof(IAbilityAction)).Single(x => x.Name.Contains(abilityType.ToString()));
        return (IAbilityAction)abilityButton.AddComponent(abilityDisplayScript);   
    }
}
