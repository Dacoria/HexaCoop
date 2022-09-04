using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class Direction
{
    public static Dictionary<DirectionType, Vector3Int> directionsOffsetOddDict = new Dictionary<DirectionType, Vector3Int>
    {
       { DirectionType.NorthWest, new Vector3Int(-1,0,1) }, //N1
       { DirectionType.NorthEast, new Vector3Int(0,0,1) }, //N2
       { DirectionType.East, new Vector3Int(1,0,0) }, //E
       { DirectionType.SouthEast, new Vector3Int(0,0,-1) }, //S2
       { DirectionType.SouthWest, new Vector3Int(-1,0,-1) }, //S1
       { DirectionType.West, new Vector3Int(-1,0,0) }, //W
    };

    public static Dictionary<DirectionType, Vector3Int> directionsOffsetEvenDict = new Dictionary<DirectionType, Vector3Int>
    {
       { DirectionType.NorthWest, new Vector3Int(0,0,1) }, //N1
       { DirectionType.NorthEast, new Vector3Int(1,0,1) }, //N2
       { DirectionType.East, new Vector3Int(1,0,0) }, //E
       { DirectionType.SouthEast, new Vector3Int(1,0,-1) }, //S2
       { DirectionType.SouthWest, new Vector3Int(0,0,-1) }, //S1
       { DirectionType.West, new Vector3Int(-1,0,0) }, //W
    };

    public static List<Vector3Int> directionsOffsetOdd => directionsOffsetOddDict.Values.ToList();
    public static List<Vector3Int> directionsOffsetEven => directionsOffsetEvenDict.Values.ToList();

    public static List<Vector3Int> GetDirectionsList(int z) => GetDirectionsDict(z).Values.ToList();

    public static Dictionary<DirectionType, Vector3Int> GetDirectionsDict(int z)
    {
        if (z % 2 == 0)
        {
            return directionsOffsetEvenDict;
        }
        else
        {
            return directionsOffsetOddDict;
        }
    }

    public static List<Vector3Int> GetDirectionsList(Hex hex) => GetDirectionsList(hex.HexCoordinates.z);
    public static Dictionary<DirectionType, Vector3Int> GetDirectionsDict(Vector3Int coor) => GetDirectionsDict(coor.z);

    public static DirectionType DeriveDirection(this Vector3Int hexFromCoor, Vector3Int hexToCoor, int steps = 1)
    {
        var diffV3 = (hexToCoor - hexFromCoor) / steps;
        var directionDic = GetDirectionsDict(hexFromCoor);

        // aanname: Dit kan altijd
        var result = directionDic.Single(x => x.Value == diffV3).Key;
        return result;
    }

    public static Vector3Int GetHexCoorInDirection(this Vector3Int hexFromCoor, DirectionType dir, int steps = 1)
    {
        var directionDic = GetDirectionsDict(hexFromCoor);
        var result = hexFromCoor + (directionDic[dir] * steps);
        return result;
    }
}

public enum DirectionType
{
    NorthWest,
    NorthEast,
    East,
    SouthWest,
    SouthEast,
    West
}