using System.Linq;
using UnityEngine;
public class SelectSwapTilesAbility : MonoBehaviour, IAbilityAction
{
    public AbilityType AbilityType => AbilityType.SwapTiles;

    private bool abilIsActive;
    public void DeselectAbility()
    {
        NeighbourHexTileSelectionManager.instance.DeselectHighlightedNeighbours();
        Utils.Destroy(GetComponents<HighlightOneTileDisplayScript>());
        abilIsActive = false;
    }

    public void InitAbilityAction()
    {
        Utils.Destroy(GetComponents<HighlightOneTileDisplayScript>());

        var highlightOneTileSelection = MonoHelper.instance.GetHighlightOneTileSelection(gameObject);
        highlightOneTileSelection.CallbackOnTileSelectionConfirmed = OnTileSelection;
    }

    private Hex firstSwapHexTile;
    private void OnTileSelection(Hex hex)
    {
        this.firstSwapHexTile = hex;

        // 2e moet neighbour zijn
        Utils.Destroy(GetComponents<HighlightOneTileDisplayScript>());
        NeighbourHexTileSelectionManager.instance.HighlightMovementOptionsAroundTile(hex, excludeObstacles: false, onlyMoveInOneDirection: false);
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

    private void OnMovementTileSelected(Hex neighbourHexSelected)
    {
        Netw.CurrPlayer().Ability(this.firstSwapHexTile, neighbourHexSelected, AbilityType);
    }

    public bool CanDoAbility(PlayerScript player) => true;
}