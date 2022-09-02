using System.Collections.Generic;

public class GridHighlight : HexaEventCallback
{
    protected override void OnNewPlayerTurn(PlayerScript playersTurn) => ClearAllHighlightsOnGrid();
    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript currPlayer) => ClearAllHighlightsOnGrid();

    private void ClearAllHighlightsOnGrid()
    {
        var allTiles = HexGrid.instance.GetAllTiles();
        foreach (var tile in allTiles)
        {
            tile.DisableHighlight();
        }
    }
}