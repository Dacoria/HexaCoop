using System.Linq;
using UnityEngine;

public class SelectArtilleryAbility : MonoBehaviour, IAbilityAction
{
    public AbilityType AbilityType => AbilityType.Artillery;

    private bool abilIsActive;
    public void DeselectAbility()
    {
        NeighbourHexTileSelectionManager.instance.DeselectHighlightedNeighbours();
        abilIsActive = false;
    }

    public void InitAbilityAction()
    {
        NeighbourHexTileSelectionManager.instance.HighlightMovementOptionsAroundTile(Netw.CurrPlayer().CurrentHexTile, excludeObstacles: false, onlyMoveInOneDirection: true, range: 20);
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
            NeighbourHexTileSelectionManager.instance.HandleMouseClickForMove(Input.mousePosition, OnMovementTileSelected);
        }
    }

    private void OnMovementTileSelected(Hex hexSelected)
    {
        Netw.CurrPlayer().Ability(hexSelected, AbilityType);
    }
}
