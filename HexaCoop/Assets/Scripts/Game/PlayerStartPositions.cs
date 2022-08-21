using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PlayerStartPositions
{
    public static List<PlayerStartPosition> GetStartTiles(Vector3Int tileRUCoor, int players = 4)
    {
        var startPos = tileRUCoor.y < 8 ? new Vector3Int(0, 0, 0): new Vector3Int(2, 0, 1);

        // bij oneven aantal rijen + z is zelf oneven, dan begint de x bij 1 ipv 0! --> vandaar extra corrigeren
        var xUnevenIncrement = (tileRUCoor.z % 2 == 0 && startPos.z % 2 == 1) ? 1 : 0;

        var secondPos = new Vector3Int(tileRUCoor.x - startPos.x + xUnevenIncrement, 0, tileRUCoor.z - startPos.z);
        //var secondPos = new Vector3Int(startPos.x + 1, 0, startPos.z + 1);
        var thirdPos = new Vector3Int(startPos.x, 0, tileRUCoor.z - startPos.z);
        var fourthPos = new Vector3Int(tileRUCoor.x - startPos.x + xUnevenIncrement, 0, startPos.z);

        var res = new List<PlayerStartPosition>
        {
            new PlayerStartPosition(0, startPos), // bottom left
            new PlayerStartPosition(1, secondPos), // top right
            new PlayerStartPosition(2, thirdPos), // bottom right
            new PlayerStartPosition(3, fourthPos) // top left
        };

        return res.Take(players).ToList();
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