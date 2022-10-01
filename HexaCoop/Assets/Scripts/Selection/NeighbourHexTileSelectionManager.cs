using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NeighbourHexTileSelectionManager : MonoBehaviour
{
    private HexGrid HexGrid;
    private List<Vector3Int> validNeighboursHightlighted = new List<Vector3Int>();

    public static NeighbourHexTileSelectionManager instance;

    private void Awake()
    {
        instance = this;
        HexGrid = FindObjectOfType<HexGrid>();
    }

    public void HighlightNeighbourTilesPlayer(bool excludeObstacles, bool onlyMoveInOneDirection, int range = 1, bool showOnlyFurthestRange = false, bool excludeWater = false, bool excludeCrystals = false)
    {
        HighlightNeighbourTiles(Netw.CurrPlayer().CurrentHexTile, excludeObstacles: excludeObstacles, onlyMoveInOneDirection: onlyMoveInOneDirection, range: range, showOnlyFurthestRange: showOnlyFurthestRange, excludeWater: excludeWater, excludeCrystals: excludeCrystals);
    }

    public void HighlightNeighbourTiles(Hex hex, bool excludeObstacles, bool onlyMoveInOneDirection, int range, bool showOnlyFurthestRange, bool excludeWater, bool excludeCrystals)
    {
        HightlightValidNeighbourTiles(hex, excludeObstacles: excludeObstacles, onlyMoveInOneDirection: onlyMoveInOneDirection, range: range, showOnlyFurthestRange: showOnlyFurthestRange, excludeWater: excludeWater, excludeCrystals: excludeCrystals);
    }

    public void HandleMouseClickForMove(Vector3 mousePosition, Action<Hex> callback)
    {
        List<Hex> selectedHexes;
        if (MonoHelper.instance.FindTile(mousePosition, out selectedHexes))
        {
            TrySelectAction(selectedHexes, callback);
            return;
        }        
       
        StopHighlightingMovementAbility(); // niks meer highlighten bij een klik
    }   

    private void TrySelectAction(List<Hex> selectedHexTiles, Action<Hex> callback)
    {
        var validNeighboursClicked = selectedHexTiles.Where(x => validNeighboursHightlighted.Any(y => y == x.HexCoordinates)).ToList();
        if (validNeighboursClicked.Count == 1)
        {
            StopHighlightingMovementAbility();
            callback(validNeighboursClicked[0]);            
        }

        //laat highlighting aan bij dubbele of geen resultaten (maar wel een tile)
    }

    private void StopHighlightingMovementAbility()
    {
        ButtonUpdater.instance.SetToUnselected(AbilityType.Movement);
        DeselectHighlightedNeighbours();
    }

    public void DeselectHighlightedNeighbours()
    {
        foreach (var neightbour in validNeighboursHightlighted)
        {
            HexGrid.GetTileAt(neightbour).DisableHighlight(HighlightActionType.SelectTile.GetColor());
            HexGrid.GetTileAt(neightbour).DisableHighlight(HighlightActionType.MoveOption.GetColor());
            HexGrid.GetTileAt(neightbour).DisableHighlight(HighlightActionType.EnemyOption.GetColor());
        }
    }

    private void HightlightValidNeighbourTiles(Hex selectedHex, bool excludeObstacles, int range, bool onlyMoveInOneDirection, bool showOnlyFurthestRange, bool excludeWater, bool excludeCrystals)
    {
        var neighboursToTryToHightlight = HexGrid.GetNeighboursFor(selectedHex.HexCoordinates, excludeObstacles: excludeObstacles, range: range, onlyMoveInOneDirection: onlyMoveInOneDirection, showOnlyFurthestRange: showOnlyFurthestRange, excludeWater: excludeWater, excludeCrystals: excludeCrystals);
        validNeighboursHightlighted = new List<Vector3Int>();

        foreach (var neightbour in neighboursToTryToHightlight)
        {
            validNeighboursHightlighted.Add(neightbour);
            HexGrid.GetTileAt(neightbour).EnableHighlight(HighlightActionType.SelectTile.GetColor());
        }
    }
}
