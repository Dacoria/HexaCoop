using UnityEngine;

public class SelectForcefieldAbility : MonoBehaviour, IAbilityAction
{
    public AbilityType AbilityType => AbilityType.Forcefield;

    public void InitAbilityAction()
    {
        DeselectAbility();
        Netw.CurrPlayer().Ability(Netw.CurrPlayer().CurrentHexTile, AbilityType);
    }
    public void DeselectAbility()
    {
        Utils.Destroy(GetComponents<HighlightOneTileDisplayScript>());
    }

    public bool CanDoAbility(PlayerScript player) =>
        player?.GetComponent<PlayerAbilityHistory>().HasDoneAbilityThisTurn(AbilityType.Forcefield) == false &&
        player?.GetComponent<PlayerForcefieldScript>() == null;
}
