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
        NeighbourHexTileSelectionManager.instance.HighlightNeighbourOptionsAroundPlayer(GameHandler.instance.CurrentPlayer());
        abilIsActive = true;
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
        NetworkAE.instance.PlayerAbility(selectedPlayer, hexSelected, AbilityType);
    }
}
