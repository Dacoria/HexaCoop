using UnityEngine;

public class SelectJumpAbility : MonoBehaviour, IAbilityAction
{
    public AbilityType AbilityType => AbilityType.Jump;

    private bool movementAbilIsActive;
    public void DeselectAbility()
    {
        NeighbourHexTileSelectionManager.instance.DeselectHighlightedNeighbours();
        movementAbilIsActive = false;
    }

    public void InitAbilityAction()
    {
        NeighbourHexTileSelectionManager.instance.HighlightMovementOptionsAroundTile(Netw.CurrPlayer().CurrentHexTile, excludeObstacles: false, onlyMoveInOneDirection: true, range: 2, showOnlyFurthestRange: true);
        movementAbilIsActive = true;
    }

    private void Update()
    {
        if(!movementAbilIsActive)
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

    public bool CanDoAbility(PlayerScript player) => player?.GetComponent<PlayerAbilityHistory>().HasDoneAbilityThisTurn(AbilityType.Meditate) == false;
}