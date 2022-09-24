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

    public static List<DirectionType> DeriveDirections(this Vector3Int hexFromCoor, Vector3Int hexToCoor)
    {
        if (hexFromCoor == hexToCoor) { return new List<DirectionType>(); }

        var path = GetPathToHex(hexFromCoor, hexToCoor);
        var result = new List<DirectionType> { GetDirectionFromCoordinates(hexFromCoor, path[0]) };

        for (var i = 1; i < path.Count(); i++)
        {
            var nextDirection = GetDirectionFromCoordinates(path[i - 1], path[i - 0]);
            result.Add(nextDirection);
        }

        return result;
    }

    private static DirectionType GetDirectionFromCoordinates(Vector3Int hexFromCoor, Vector3Int hexToCoor)
    {
        var diffV3 = hexToCoor - hexFromCoor;
        var directionDic = GetDirectionsDict(hexFromCoor);

        var result = directionDic.Single(x => x.Value == diffV3).Key;
        return result;
    }

    public static int GetRangeFromCoordinates(Vector3Int from, Vector3Int to) => GetPathToHex(from, to).Count();

    private static List<Vector3Int> GetPathToHex(Vector3Int from, Vector3Int to)
    {
        var astar = new AStarSearch(from, to, excludeObstaclesFromPath: false);
        var path = astar.FindPath();
        return path;
    }

    public static Vector3Int GetNewHexCoorFromDirection(this Vector3Int hexFromCoor, DirectionType dir, int steps = 1)
    {
        var dirs = new List<DirectionType>();
        for (var i = 0; i < steps; i++)
        {
            dirs.Add(dir);
        }

        return hexFromCoor.GetNewHexCoorFromDirections(dirs);
    }

    public static Vector3Int GetNewHexCoorFromDirections(this Vector3Int hexFromCoor, List<DirectionType> dirs)
    {
        var nextHex = hexFromCoor;
        foreach (var dir in dirs)
        {
            nextHex = GetHexCoorFromDirection(nextHex, dir);
        }

        return nextHex;
    }

    private static Vector3Int GetHexCoorFromDirection(this Vector3Int hexFromCoor, DirectionType dir)
    {
        var directionDic = GetDirectionsDict(hexFromCoor);
        var result = hexFromCoor + directionDic[dir];
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