using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NeighbourHexTileSelectionManager : MonoBehaviour
{
    public HexGrid HexGrid;
    private List<Vector3Int> validNeighboursHightlighted = new List<Vector3Int>();

    public PlayerScript SelectedPlayer;
    public static NeighbourHexTileSelectionManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void HighlightMovementOptionsAroundPlayer(PlayerScript player)
    {
        SelectedPlayer = player;
        onlyHighlightColor = false;
        HightlightValidNeighbourTiles(player.CurrentHexTile, excludeObstacles: true);
    }

    private bool onlyHighlightColor = false;

    public void HighlightNeighbourOptionsAroundPlayer(PlayerScript player, bool? withTargetOnTile = null)
    {
        SelectedPlayer = player;
        onlyHighlightColor = true;
        HightlightValidNeighbourTiles(player.CurrentHexTile, excludeObstacles: false);
    }

    public void HandleMouseClickForMove(Vector3 mousePosition, Action<PlayerScript, Hex> callback)
    {
        List<Hex> selectedHexes;
        if (MonoHelper.instance.FindTile(mousePosition, out selectedHexes))
        {
            if (SelectedPlayer != null && Netw.CurrPlayer() == SelectedPlayer)
            {
                TryPlayerMoveAction(selectedHexes, callback);
                return;
            }
        }        
       
        StopHighlightingMovementAbility(); // niks meer highlighten bij een klik
        SelectedPlayer = null;
    }   

    private void TryPlayerMoveAction(List<Hex> selectedHexTiles, Action<PlayerScript, Hex> callback)
    {
        var validNeighboursClicked = selectedHexTiles.Where(x => validNeighboursHightlighted.Any(y => y == x.HexCoordinates)).ToList();
        if (validNeighboursClicked.Count == 1 && SelectedPlayer != null)
        {
            StopHighlightingMovementAbility();
            callback(SelectedPlayer, validNeighboursClicked[0]);            
            SelectedPlayer = null;
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

    private void HightlightValidNeighbourTiles(Hex selectedHex, bool excludeObstacles)
    {
        var neighboursToTryToHightlight = HexGrid.GetNeighboursFor(selectedHex.HexCoordinates, excludeObstacles: excludeObstacles);
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
            if(neightbour.GetHex().HasUnit())
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
