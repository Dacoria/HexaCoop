using System.Linq;
using UnityEngine;
public class SelectSwapTilesAbility : MonoBehaviour, IAbilityAction
{
    public AbilityType AbilityType => AbilityType.SwapTiles;

    private bool abilIsActive;
    public void DeselectAbility()
    {
        NeighbourHexTileSelectionManager.instance.DeselectHighlightedNeighbours();
        firstSwapHexTile = null;
        abilIsActive = false;
    }

    public void InitAbilityAction()
    {
        Utils.Destroy(GetComponents<HighlightOneTileDisplayScript>());
        NeighbourHexTileSelectionManager.instance.HighlightNeighbourTilesPlayer(
            range: 2,
            excludeObstacles: false,
            onlyMoveInOneDirection: false,
            excludeCrystals: true,
            excludeWater: true,
            showOnlyFurthestRange: false
        );
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
            NeighbourHexTileSelectionManager.instance.HandleMouseClickForMove(Input.mousePosition, OnTileSelected);
        }
    }

    private void OnTileSelected(Hex neighbourHexSelected)
    {
        NeighbourHexTileSelectionManager.instance.DeselectHighlightedNeighbours();
        if(firstSwapHexTile == null)
        {
            OnFirstTileSelect(neighbourHexSelected);
        }
        else
        {
            Netw.CurrPlayer().Ability(this.firstSwapHexTile, neighbourHexSelected, AbilityType);
        }
    }

    private Hex firstSwapHexTile;
    private void OnFirstTileSelect(Hex hex)
    {
        this.firstSwapHexTile = hex;

        // 2e moet neighbour zijn
        Utils.Destroy(GetComponents<HighlightOneTileDisplayScript>());
        NeighbourHexTileSelectionManager.instance.HighlightNeighbourTiles(
            hex,
            range: 1,
            excludeObstacles: false,
            onlyMoveInOneDirection: false,
            excludeCrystals: true,
            excludeWater: true,
            showOnlyFurthestRange: false
        );


        abilIsActive = true;
    }

    public bool CanDoAbility(PlayerScript player) => true;
}