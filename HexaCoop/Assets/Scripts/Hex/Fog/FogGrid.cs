using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

public class FogGrid : HexaEventCallback
{
    [ComponentInject] private HexGrid hexGrid;

    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript playersTurn)
    {
        // initiele setup --> daarna OnNewPlayerTurn voor de beurt updaten
        UpdatePlayersFog(new List<PlayerScript> { Netw.MyPlayer() });
    }

    protected override void OnNewPlayerTurn(PlayerScript playersTurn)
    {
        if (playersTurn.IsOnMyNetwork())
        {
            UpdateVisibility(playersTurn); // om bewegen van de ander te voorkomen (dat zie je)
        }
        else
        {
            // op deze manier, ook om AI goed te ondersteunen
            UpdateVisibility(Netw.MyPlayer());
        }
    }

    protected override void OnPlayerAbility(PlayerScript player, Hex hex, AbilityType abilityType)
    {
        if (player.IsOnMyNetwork() && abilityType == AbilityType.Binocular)
        {
            UpdateVisibility(player);
        }
    }

    protected override void OnPlayerMovingFinished(PlayerScript player) => UpdatePlayerAfterNewPosInTurn(player);
    protected override void OnPlayerScriptHasTeleported(PlayerScript player, Hex hex) => UpdatePlayerAfterNewPosInTurn(player);

    private void UpdatePlayerAfterNewPosInTurn(PlayerScript player)
    {
        if (player.IsOnMyNetwork())
        {
            UpdatePlayersFog(new List<PlayerScript> { player });
        }
        else
        {
            // voor als je beweegt binnen de bestaande range ve andere speler!
            UpdatePlayersVisibleNow();
        }
    }

    private void UpdatePlayersVisibleNow()
    {
        foreach (var player in GameHandler.instance.AllPlayers)
        {
            player?.PlayerModel.gameObject.SetActive(!player.CurrentHexTile.FogIsActive());
        }
    }

    protected override void OnEnemyFaseStarted() => UpdatePlayersFog(Netw.PlayersOnMyNetwork());
    public void UpdateVisibility(PlayerScript player) => UpdatePlayersFog(new List<PlayerScript> { player });
    private void UpdatePlayersFog(List<PlayerScript> players) => StartCoroutine(UpdatePlayersFog(players, 0.1f)); // wachten op visuele verwerking 

    private IEnumerator UpdatePlayersFog(List<PlayerScript> players, float secondsToWait)
    {
        yield return Wait4Seconds.Get(secondsToWait);
        var noFogTiles = new List<Hex>();

        foreach(var player in players)
        {
            var playerTile = player.CurrentHexTile;
            noFogTiles.AddRange(hexGrid.GetNeighboursFor(playerTile.HexCoordinates, range: player.GetVision(), excludeObstacles: false).Select(x => x.GetHex()).ToList());
            noFogTiles.Add(playerTile);
        }

        SetFog(noFogTiles);
    }

    private void SetFog(List<Hex> visibleTiles)
    {
        var allTiles = hexGrid.GetAllTiles();
        foreach (var tile in allTiles)
        {
            var tileIsVisible = Settings.ShowEverything || visibleTiles.Contains(tile);
            tile.SetFogOnHex(!tileIsVisible);           
                
            EnemyManager.instance.GetEnemies().Where(x => x.CurrentHexTile == tile).ToList().ForEach(enemy =>
            {
                enemy.SetVisible(Settings.ShowEnemiesInFog || tileIsVisible);
            });            
        }

        UpdatePlayersVisibleNow();
    }
}