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

    public void HighlightMovementOptionsAroundTile(Hex hex, bool excludeObstacles, bool onlyMoveInOneDirection, int range = 1, bool showOnlyFurthestRange = false)
    {
        onlyHighlightColor = false;
        HightlightValidNeighbourTiles(hex, excludeObstacles: excludeObstacles, onlyMoveInOneDirection: onlyMoveInOneDirection, range: range, showOnlyFurthestRange: showOnlyFurthestRange);
    }

    private bool onlyHighlightColor = false;

    public void HighlightNeighbourOptionsAroundTile(Hex hex)
    {
        onlyHighlightColor = true;
        HightlightValidNeighbourTiles(hex, excludeObstacles: false, range: 1) ;
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

    private void HightlightValidNeighbourTiles(Hex selectedHex, bool excludeObstacles, int range, bool onlyMoveInOneDirection = false, bool showOnlyFurthestRange = false)
    {
        var neighboursToTryToHightlight = HexGrid.GetNeighboursFor(selectedHex.HexCoordinates, excludeObstacles: excludeObstacles, range: range, onlyMoveInOneDirection: onlyMoveInOneDirection, showOnlyFurthestRange: showOnlyFurthestRange);
        validNeighboursHightlighted = new List<Vector3Int>();

        foreach (var neightbour in neighboursToTryToHightlight)
        {
            validNeighboursHightlighted.Add(neightbour);
            SetHighlightColor(neightbour);
        }
    }

    private void SetHighlightColor(Vector3Int neightbour)
    {
        if(onlyHighlightColor)
        {
            HexGrid.GetTileAt(neightbour).EnableHighlight(HighlightActionType.SelectTile.GetColor());
        }
        else
        {
            if(neightbour.GetHex().HasUnit(isAlive: true))
            {
                HexGrid.GetTileAt(neightbour).EnableHighlight(HighlightActionType.EnemyOption.GetColor());
            }
            else
            {
                HexGrid.GetTileAt(neightbour).EnableHighlight(HighlightActionType.MoveOption.GetColor());
            }
        }
    }
}
