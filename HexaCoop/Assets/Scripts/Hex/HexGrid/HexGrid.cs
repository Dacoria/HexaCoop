using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HexGrid : MonoBehaviour
{
    private Dictionary<Vector3Int, Hex> hexTileDict = new Dictionary<Vector3Int, Hex>();

    // cache
    private Dictionary<Vector3Int, List<Vector3Int>> hexTileNeightboursDict = new Dictionary<Vector3Int, List<Vector3Int>>();

    public static HexGrid instance;

    public FogOnHex FogPrefab;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        var hexes = FindObjectsOfType<Hex>();        
        var hexesSorted = hexes.OrderBy(x => Vector3.Distance(x.transform.position, new Vector3(0,0,0))).ToList();

        foreach (var hex in hexesSorted)
        {
            if(hexTileDict.ContainsKey(hex.HexCoordinates))
            {
                throw new Exception(hex.gameObject.name + " + " + hexTileDict[hex.HexCoordinates].gameObject.name);
            }

            if (hex.transform.GetChild(0).childCount != 1)
            {
                throw new Exception(hex.gameObject.name + " heeft meerdere tiles" );
            }    

            hexTileDict[hex.HexCoordinates] = hex;

            Instantiate(FogPrefab, hex.transform);

            var lerp = hex.gameObject.AddComponent<LerpMovement>();
            lerp.MoveToDestination(hex.transform.position, duration: 1.5f, startPosition: hex.transform.position + new Vector3(0, -100, 0), delayedStart: hex.transform.position.x * 0.08f);
        }
    }    

    private bool HexGridLoaded;

    private void Update()
    {
        if(!HexGridLoaded)
        {
            HexGridLoaded = hexTileDict.Values.All(x => Vector3.Distance(x.OrigPosition, x.transform.position) < 0.01f);
            if(HexGridLoaded)
            {
                ActionEvents.GridLoaded?.Invoke(); 
            }
        }
    }

    public bool IsLoaded() => HexGridLoaded;

    // voor A*
    public float Cost(Vector3Int current, Vector3Int directNeighbor) => 1;

    public List<Hex> GetAllTiles() => hexTileDict.Values.ToList();

    public List<Hex> GetTiles(HighlightColorType type) => hexTileDict.Values.Where(x => x.GetHighlight().HasValue && x.GetHighlight().Value == type).ToList();

    public Hex GetTileAt(Vector3Int hexCoordinates)
    {
        Hex result = null;
        hexTileDict.TryGetValue(hexCoordinates, out result);
        return result;
    }

    public List<Vector3Int> GetNeighboursFor(Vector3Int hexCoordinates, int range = 1, bool excludeObstacles = true, bool? withUnitOnTile = null)
    {
        var neighbours = GetNeighboursFor(hexCoordinates, range);
        if(excludeObstacles)
        {
            neighbours = neighbours.Where(x => !x.GetHex().IsObstacle()).ToList();
        }
        if(withUnitOnTile.HasValue)
        {
            neighbours = neighbours.Where(x => x.GetHex().HasUnit() == withUnitOnTile.Value).ToList();
        }

        return neighbours;
    }

    public Hex GetTileRightUpperCorner()
    {
        var hexRightUpperCornerCoordinate = hexTileDict.OrderByDescending(z => z.Key.z)
            .ThenByDescending(x => x.Key.x)
            .First().Key;

        return hexTileDict[hexRightUpperCornerCoordinate];
    }

    private List<Vector3Int> GetNeighboursFor(Vector3Int hexCoordinates, int range)
    {
        if (range <= 0)
        {
            return new List<Vector3Int>();
        }

        var neighboursRange1 = GetNeighboursFor(hexCoordinates);     

        var totalProcessedUniqueList = neighboursRange1;
        var previousRankUniqueList = neighboursRange1;

        for (int currentRank = 2; currentRank <= range && currentRank <= 10; currentRank++)
        {
            var newUniqueList = GetUniqueNeighboursNotVisited(hexCoordinates, totalProcessedUniqueList, previousRankUniqueList);
            totalProcessedUniqueList = totalProcessedUniqueList.Concat(newUniqueList).ToList();
            previousRankUniqueList = newUniqueList.ToList();
        }

        return totalProcessedUniqueList;
    }

    private HashSet<Vector3Int> GetUniqueNeighboursNotVisited(Vector3Int startHexToExclude, List<Vector3Int> previouslyVisited, List<Vector3Int> previousRankUniqueList)
    {
        var newUniqueList = new HashSet<Vector3Int>();

        foreach (var neightbourRange in previousRankUniqueList)
        {
            var neighboursOfPreviousRank = GetNeighboursFor(neightbourRange);
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

    private List<Vector3Int> GetNeighboursFor(Vector3Int hexCoordinates)
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

    public bool IsOnEdgeOfGrid(Vector3Int hexCoordinate)
    {
        if (!hexTileDict.ContainsKey(new Vector3Int(hexCoordinate.x + 1, hexCoordinate.y, hexCoordinate.z)))
        {
            return true;
        }
        if (!hexTileDict.ContainsKey(new Vector3Int(hexCoordinate.x - 1, hexCoordinate.y, hexCoordinate.z)))
        {
            return true;
        }
        if (!hexTileDict.ContainsKey(new Vector3Int(hexCoordinate.x, hexCoordinate.y, hexCoordinate.z + 1)))
        {
            return true;
        }
        if (!hexTileDict.ContainsKey(new Vector3Int(hexCoordinate.x, hexCoordinate.y, hexCoordinate.z - 1)))
        {
            return true;
        }

        return false;
    }

    public bool IsOnCornerOfGrid(Vector3Int hexCoordinate)
    {
        if (!hexTileDict.ContainsKey(new Vector3Int(hexCoordinate.x + 1, hexCoordinate.y, hexCoordinate.z))
            &&
            !hexTileDict.ContainsKey(new Vector3Int(hexCoordinate.x, hexCoordinate.y, hexCoordinate.z + 1)))
        {
            return true;
        }
        if (!hexTileDict.ContainsKey(new Vector3Int(hexCoordinate.x - 1, hexCoordinate.y, hexCoordinate.z))
            &&
            !hexTileDict.ContainsKey(new Vector3Int(hexCoordinate.x, hexCoordinate.y, hexCoordinate.z - 1)))
        {
            return true;
        }

        return false;
    }
}