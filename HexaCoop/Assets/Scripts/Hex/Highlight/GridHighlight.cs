using System.Collections.Generic;

public class GridHighlight : HexaEventCallback
{
    protected override void OnNewPlayerTurn(PlayerScript playersTurn)
    {
        ClearAllHighlightsOnGrid();
    }

    protected override void OnNewRoundStarted(List<PlayerScript> arg1, PlayerScript arg2)
    {
        ClearAllHighlightsOnGrid();
    }

    private void ClearAllHighlightsOnGrid()
    {
        var allTiles = HexGrid.instance.GetAllTiles();
        foreach (var tile in allTiles)
        {
            tile.DisableHighlight();
        }
    }
}