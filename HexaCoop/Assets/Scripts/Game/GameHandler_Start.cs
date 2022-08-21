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
        var players = NetworkHelper.instance.GetAllPlayers().OrderBy(x => x.Id).Take(GetStartTiles().Count).ToList();
        currentPlayer = players[0];
        NetworkAE.instance.NewRoundStarted(players, CurrentPlayer());
    }

    public void ResetGame()
    {
        StartCoroutine(CR_ResetGame());
    }

    public IEnumerator CR_ResetGame()
    {
        yield return new WaitForSeconds(0.1f);
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
        AllPlayers = NetworkHelper.instance.GetAllPlayers().Take(GetStartTiles().Count).ToList();

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
            var startHexTile = HexGrid.GetTileAt(GetStartTiles()[i]);
            AllPlayers[i].transform.position = new Vector3(startHexTile.transform.position.x, 0, startHexTile.transform.position.z); // vanwege grids omhoog komen
            AllPlayers[i].SetCurrentHexTile(startHexTile);
        }
    }

    public List<Vector3Int> GetStartTiles(int players = 4)
    {
        var tileRUCoor = HexGrid.instance.GetTileRightUpperCorner().HexCoordinates;       

        var startPos = tileRUCoor.y < 8 ? new Vector3Int(0, 0, 0): new Vector3Int(2, 0, 1);

        // bij oneven aantal rijen + z is zelf oneven, dan begint de x bij 1 ipv 0! --> vandaar extra corrigeren
        var xUnevenIncrement = (tileRUCoor.z % 2 == 0 && startPos.z % 2 == 1) ? 1 : 0;

        var secondPos = new Vector3Int(tileRUCoor.x - startPos.x + xUnevenIncrement, 0, tileRUCoor.z - startPos.z);
        //var secondPos = new Vector3Int(startPos.x + 1, 0, startPos.z + 1);
        var thirdPos = new Vector3Int(startPos.x, 0, tileRUCoor.z - startPos.z);
        var fourthPos = new Vector3Int(tileRUCoor.x - startPos.x + xUnevenIncrement, 0, startPos.z);

        var res = new List<Vector3Int>
        {
            startPos, // bottom left
            secondPos, // top right
            thirdPos, // bottom right
            fourthPos // top left
        };

        return res.Take(players).ToList();
    }
}