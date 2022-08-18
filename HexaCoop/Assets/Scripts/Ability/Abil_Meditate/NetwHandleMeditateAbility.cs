
public class NetwHandleMeditateAbility : HexaEventCallback, IAbilityNetworkHandler
{    
    public void NetworkHandle(PlayerScript player, Hex hexNearMe)
    {
        var extraApScript = player.gameObject.AddComponent<PlayerExtraAPScript>();
        extraApScript.AdditionalAP = 2;

        ClearAllRadars();

        var gridsAroundRadarSpot = HexGrid.instance.GetNeighboursFor(hexNearMe.HexCoordinates, excludeObstacles: false);
        hexNearMe.EnableHighlight(HighlightColorType.Blue);

        foreach (var grid in gridsAroundRadarSpot)
        {
            grid.GetHex().EnableHighlight(HighlightColorType.Blue);
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