using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class GameHandler : HexaEventCallback
{
    public List<PlayerScript> AllPlayers;

    private void SetupNewGame()
    {
        var players = NetworkHelper.instance.GetAllPlayers().OrderBy(x => x.Id).Take(GetStartTilesCount()).ToList();
        currentPlayer = players[0];
        NetworkAE.instance.NewRoundStarted(players, CurrentPlayer());
    }

    public void ResetGame()
    {
        StartCoroutine(CR_ResetGame());
    }

    public IEnumerator CR_ResetGame()
    {
        yield return Wait4Seconds.Get(0.1f);
        if (HexGrid.IsLoaded() && NetworkHelper.instance.GetAllPlayers().Count > 0)
        {
            SetupNewGame();
        }
        else
        {
            StartCoroutine(CR_ResetGame());
        }
    }

    protected override void OnNewRoundStarted(List<PlayerScript> players, PlayerScript currPlayer)
    {
        GameStatus = GameStatus.ActiveRound;

        // refresh om te checken
        AllPlayers = NetworkHelper.instance.GetAllPlayers().Take(GetStartTilesCount()).ToList();

        // check
        var playersMatch = players.Select(x => x.Id).All(AllPlayers.Select(x => x.Id).Contains);
        var sameSize = players.Count == AllPlayers.Count;
        if (!playersMatch || !sameSize) { throw new Exception(); }

        // fix order....
        var playersRes = new List<PlayerScript>();
        for (int i = 0; i < players.Count; i++)
        {
            playersRes.Add(AllPlayers.Single(x => x.Id == players[i].Id));
        }
        AllPlayers = playersRes;

        // reset local
        ResetGameLocal();

        currentPlayer = currPlayer;
        CurrentTurn = 1;
    }

    private void ResetGameLocal()
    {
        for (int i = 0; i < AllPlayers.Count; i++)
        {
            var startTileData = GetStartTile(index: i);
            var startHexTile = HexGrid.GetTileAt(startTileData.Position);
            AllPlayers[i].transform.position = new Vector3(startHexTile.transform.position.x, 0, startHexTile.transform.position.z); // vanwege grids omhoog komen
            AllPlayers[i].SetCurrentHexTile(startHexTile);
            AllPlayers[i].Index = i;
        }
    }

    public int GetStartTilesCount()
    {
        var tileRUCoor = HexGrid.instance.GetTileRightUpperCorner().HexCoordinates;
        return PlayerStartPositions.GetStartTiles(tileRUCoor).Count();
    }

    public PlayerStartPosition GetStartTile(int index)
    {
        var tileRUCoor = HexGrid.instance.GetTileRightUpperCorner().HexCoordinates;       
        return PlayerStartPositions.GetStartTiles(tileRUCoor).Single(x => x.Index == index);
    }
}