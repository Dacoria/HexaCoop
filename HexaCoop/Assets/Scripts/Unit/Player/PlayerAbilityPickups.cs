using System.Linq;
using System.Collections.Generic;

public class PlayerAbilityPickups : HexaEventCallback
{
    [ComponentInject] private PlayerScript playerScript;

    private List<AbilityType> AbilityPickups;

    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript currentPlayer)
    {
        AbilityPickups = new List<AbilityType>();
    }

    protected override void OnPlayerAbility(PlayerScript player, Hex hex, Hex hex2, AbilityType abilityType, int queueId)
    {
        if (player == playerScript)
        {
            if (AbilityPickups.Any(x => x == abilityType))
            {
                AbilityPickups.Remove(abilityType);
            }
        }
    }

    public bool HasPickupAbility(AbilityType ability) => AbilityPickups.Any(x => x == ability);
    public void AddPickupAbility(AbilityType ability) => AbilityPickups.Add(ability);
}