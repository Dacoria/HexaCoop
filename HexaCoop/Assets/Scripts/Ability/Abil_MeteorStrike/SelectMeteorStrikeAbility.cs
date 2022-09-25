using System.Linq;
using UnityEngine;

public class SelectMeteorStrikeAbility : MonoBehaviour, IAbilityAction
{
    public AbilityType AbilityType => AbilityType.MeteorStrike;

    private bool abilIsActive;
    public void DeselectAbility()
    {
        NeighbourHexTileSelectionManager.instance.DeselectHighlightedNeighbours();
        abilIsActive = false;
    }

    public void InitAbilityAction()
    {
        Netw.CurrPlayer().Ability(Netw.CurrPlayer().CurrentHexTile, AbilityType);
    }

    private void Update()
    {
        if (!abilIsActive)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (NeighbourHexTileSelectionManager.instance.SelectedPlayer == null)
            {
                return;
            }

            NeighbourHexTileSelectionManager.instance.HandleMouseClickForMove(Input.mousePosition, OnMovementTileSelected);
        }
    }

    private void OnMovementTileSelected(PlayerScript selectedPlayer, Hex hexSelected)
    {
        selectedPlayer.Ability(hexSelected, AbilityType);
    }
}
