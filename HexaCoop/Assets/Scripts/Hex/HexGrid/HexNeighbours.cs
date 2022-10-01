using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HexNeighbours
{
    // cache
    private Dictionary<Vector3Int, List<Vector3Int>> hexTileNeightboursDict = new Dictionary<Vector3Int, List<Vector3Int>>();

    public List<Vector3Int> GetNeighboursFor(Dictionary<Vector3Int, Hex> hexTileDict, Vector3Int hexCoordinates, int range, bool excludeObstacles, bool? withUnitOnTile, bool onlyMoveInOneDirection, bool showOnlyFurthestRange, bool includeStartHex, bool excludeWater = false, bool excludeCrystals = false)
    {
        var neighbours = GetNeighboursForWithDirection(hexTileDict, hexCoordinates, range, onlyMoveInOneDirection);

        if (showOnlyFurthestRange)
        {            
            neighbours = neighbours.Where(neighbour => Direction.GetRangeFromCoordinates(hexCoordinates, neighbour) == range).ToList();
        }
        if(excludeObstacles)
        {
            neighbours = neighbours.Where(neighbour => !neighbour.GetHex().IsObstacle()).ToList();
        }
        if (excludeCrystals)
        {
            neighbours = neighbours.Where(neighbour => neighbour.GetHex().HexStructureType != HexStructureType.Crystal).ToList();
        }
        if (excludeWater)
        {
            neighbours = neighbours.Where(neighbour => !neighbour.GetHex().HexSurfaceType.IsWater()).ToList();
        }
        if (withUnitOnTile.HasValue)
        {
            neighbours = neighbours.Where(neighbour => neighbour.GetHex().HasUnit(isAlive: true) == withUnitOnTile.Value).ToList();
        }
        if(includeStartHex)
        {
            neighbours.Add(hexCoordinates);
        }

        return neighbours;
    }

    private List<Vector3Int> GetNeighboursForWithDirection(Dictionary<Vector3Int, Hex> hexTileDict, Vector3Int hexCoordinates, int range, bool onlyMoveInOneDirection)
    {
        if(onlyMoveInOneDirection)
        {
            return GetNeighboursOneDirections(hexTileDict, hexCoordinates, range);
        }
        else
        {
            return GetNeighboursAllDirections(hexTileDict, hexCoordinates, range);
        }
    }

    private List<Vector3Int> GetNeighboursOneDirections(Dictionary<Vector3Int, Hex> hexTileDict, Vector3Int startHexCoor, int range)
    {
        if (range <= 0)
        {
            return new List<Vector3Int>();
        }

        var result = new List<Vector3Int>();

        foreach (DirectionType direction in Enum.GetValues(typeof(DirectionType)))
        {
            for(int step = 1; step <= range; step++)
            {
                var newHexCoor = startHexCoor.GetNewHexCoorFromDirection(direction, step);
                if (hexTileDict.ContainsKey(newHexCoor))
                {
                    result.Add(newHexCoor);
                }
            }
        }

        return result;
    }

    private List<Vector3Int> GetNeighboursAllDirections(Dictionary<Vector3Int, Hex> hexTileDict, Vector3Int hexCoordinates, int range)
    {
        if (range <= 0)
        {
            return new List<Vector3Int>();
        }

        var neighboursRange1 = GetNeighboursFor(hexTileDict, hexCoordinates);     

        var totalProcessedUniqueList = neighboursRange1;
        var previousRankUniqueList = neighboursRange1;

        for (int currentRank = 2; currentRank <= range && currentRank <= 10; currentRank++)
        {
            var newUniqueList = GetUniqueNeighboursNotVisited(hexTileDict, hexCoordinates, totalProcessedUniqueList, previousRankUniqueList);
            totalProcessedUniqueList = totalProcessedUniqueList.Concat(newUniqueList).ToList();
            previousRankUniqueList = newUniqueList.ToList();
        }

        return totalProcessedUniqueList;
    }

    private HashSet<Vector3Int> GetUniqueNeighboursNotVisited(Dictionary<Vector3Int, Hex> hexTileDict, Vector3Int startHexToExclude, List<Vector3Int> previouslyVisited, List<Vector3Int> previousRankUniqueList)
    {
        var newUniqueList = new HashSet<Vector3Int>();

        foreach (var neightbourRange in previousRankUniqueList)
        {
            var neighboursOfPreviousRank = GetNeighboursFor(hexTileDict, neightbourRange);
            foreach (var neighbourOfNeighbor in neighboursOfPreviousRank)
            {
                if (!previouslyVisited.Any(x => x == neighbourOfNeighbor) && neighbourOfNeighbor != startHexToExclude)
                {
                    newUniqueList.Add(neighbourOfNeighbor);
                }
            }
        }

        return newUniqueList;
    }

    private List<Vector3Int> GetNeighboursFor(Dictionary<Vector3Int, Hex> hexTileDict, Vector3Int hexCoordinates)
    {
        if(!hexTileDict.ContainsKey(hexCoordinates))
        {
            return new List<Vector3Int>();
        }
        if(hexTileNeightboursDict.ContainsKey(hexCoordinates))
        {
            return hexTileNeightboursDict[hexCoordinates];
        }

        hexTileNeightboursDict.Add(hexCoordinates, new List<Vector3Int>());
        foreach(var direction in Direction.GetDirectionsList(hexCoordinates.z))
        {
            if(hexTileDict.ContainsKey(hexCoordinates + direction))
            {
                hexTileNeightboursDict[hexCoordinates].Add(hexCoordinates + direction);
            }
        }

        return hexTileNeightboursDict[hexCoordinates];
    }
}