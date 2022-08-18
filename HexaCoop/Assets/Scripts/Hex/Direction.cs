using System.Collections.Generic;
using UnityEngine;

public static class Direction
{
    public static List<Vector3Int> directionsOffsetOdd = new List<Vector3Int>
    {
       new Vector3Int(-1,0,1), //N1
       new Vector3Int(0,0,1), //N2
       new Vector3Int(1,0,0), //E
       new Vector3Int(0,0,-1), //S2
       new Vector3Int(-1,0,-1), //S1
       new Vector3Int(-1,0,0), //W
    };

    public static List<Vector3Int> directionsOffsetEven = new List<Vector3Int>
    {
       new Vector3Int(0,0,1), //N1
       new Vector3Int(1,0,1), //N2
       new Vector3Int(1,0,0), //E
       new Vector3Int(1,0,-1), //S2
       new Vector3Int(0,0,-1), //S1
       new Vector3Int(-1,0,0), //W
    };

    public static List<Vector3Int> GetDirectionsList(int z)
    {        
        if (z % 2 == 0)
        {
            return directionsOffsetEven;
        }
        else
        {
            return directionsOffsetOdd;
        }
    }
}