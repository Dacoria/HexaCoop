using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PlayerStartPositions
{
    public static List<PlayerStartPosition> GetStartTiles(Vector3Int tileRUCoor, int maxPlayers = 4)
    {
        var res = new List<PlayerStartPosition>();
        var allHexes = GameObject.FindObjectsOfType<Hex>().ToList();

        foreach (HexStartPlayerType hexStartPlayer in Enum.GetValues(typeof(HexStartPlayerType)))
        {
            if(hexStartPlayer == HexStartPlayerType.None)
            {
                continue;
            }

            if(res.Count >= maxPlayers)
            {
                break;
            }

            var hexForStartPlayer = allHexes.Single(x => x.HexStartPlayer == hexStartPlayer);
            var item = new PlayerStartPosition(hexStartPlayer.GetIndex(), hexForStartPlayer.HexCoordinates);
            res.Add(item);
        }

        return res;
    }
}

public class PlayerStartPosition
{
    public int Index;
    public Vector3Int Position;

    public PlayerStartPosition(int index, Vector3Int position)
    {
        Index = index;
        Position = position;
    }
}