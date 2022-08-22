using System.Linq;

public class NetwHandleRadarAbility : HexaEventCallback, IAbilityNetworkHandler
{
    public void NetworkHandle(PlayerScript player, Hex hexNearPlayer)
    {
        ClearAllRadars();

        var gridsAroundRadarSpot = HexGrid.instance.GetNeighboursFor(hexNearPlayer.HexCoordinates, excludeObstacles: false);
        if (player.IsOnMyNetwork() || gridsAroundRadarSpot.Any(x => x.GetHex().GetPlayer()?.IsOnMyNetwork() == true))
        {
            hexNearPlayer.EnableHighlight(HighlightColorType.Blue);

            foreach (var grid in gridsAroundRadarSpot)
            {
                grid.GetHex().EnableHighlight(HighlightColorType.Blue);
            }
        }
    }

    private void ClearAllRadars()
    {
        var allTiles = HexGrid.instance.GetAllTiles();
        foreach (var tile in allTiles)
        {
            tile.DisableHighlight(HighlightColorType.Blue);
        }
    }
}