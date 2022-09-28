using System.Linq;
using System.Collections.Generic;

public partial class PlayerAbilityHandler : HexaEventCallback
{
    [ComponentInject] private PlayerScript playerScript;

    private Dictionary<AbilityType, IAbilityNetworkHandler> dictAbilityHandlers = new Dictionary<AbilityType,IAbilityNetworkHandler>();

    protected override void OnPlayerAbility(PlayerScript player, Hex hex, Hex hex2, AbilityType abilityType, int queueId)
    {
        if (player == playerScript)
        {
            if(!dictAbilityHandlers.ContainsKey(abilityType))
            {
                CreateAbilityHandler(abilityType);
            }

            dictAbilityHandlers[abilityType].NetworkHandle(player, hex, hex2);
        }
    }

    private void CreateAbilityHandler(AbilityType abilityType)
    {
        var abilityDisplayScript = TypeUtil.GetTypesAssignableFrom(typeof(IAbilityNetworkHandler)).Single(x => x.Name.Contains(abilityType.ToString()));
        var newAbilHandler = (IAbilityNetworkHandler)gameObject.AddComponent(abilityDisplayScript);
        dictAbilityHandlers.Add(abilityType, newAbilHandler);
    }

    public bool CanDoAbility(Hex hex, Hex hex2, AbilityType abilityType)
    {
        if (!dictAbilityHandlers.ContainsKey(abilityType))
        {
            CreateAbilityHandler(abilityType);
        }

        return dictAbilityHandlers[abilityType].CanDoAbility(playerScript, hex, hex2);
    }
}