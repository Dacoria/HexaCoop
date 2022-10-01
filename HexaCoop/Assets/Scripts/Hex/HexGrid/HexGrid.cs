using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HexGrid : HexaEventCallback
{
    private Dictionary<Vector3Int, Hex> hexTileDict = new Dictionary<Vector3Int, Hex>();


    public static HexGrid instance;

    public FogOnHex FogPrefab;

    private HexNeighbours hexNeighbours = new HexNeighbours();

    private new void Awake()
    {
        base.Awake();
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

    public List<Vector3Int> GetNeighboursFor(Vector3Int hexCoordinates, int range = 1, bool excludeObstacles = true, bool? withUnitOnTile = null, bool onlyMoveInOneDirection = false, bool showOnlyFurthestRange = false, bool includeStartHex = false, bool excludeWater = false, bool excludeCrystals = false)
    {
        return hexNeighbours.GetNeighboursFor(
            hexTileDict: hexTileDict,
            hexCoordinates: hexCoordinates,
            range: range,
            excludeObstacles: excludeObstacles,
            withUnitOnTile: withUnitOnTile,
            onlyMoveInOneDirection: onlyMoveInOneDirection,
            showOnlyFurthestRange: showOnlyFurthestRange,
            includeStartHex: includeStartHex,
            excludeWater: excludeWater,
            excludeCrystals: excludeCrystals
        );
    }
    

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

    public Hex GetTileRightUpperCorner()
    {
        var hexRightUpperCornerCoordinate = hexTileDict.OrderByDescending(z => z.Key.z)
            .ThenByDescending(x => x.Key.x)
            .First().Key;

        return hexTileDict[hexRightUpperCornerCoordinate];
    }   

    protected override void OnPlayerAbility(PlayerScript player, Hex hex, Hex hex2, AbilityType abilityType, int queueId)
    {
        if(abilityType == AbilityType.SwapTiles)
        {
            hexTileDict[hex.HexCoordinates] = hex2;
            hexTileDict[hex2.HexCoordinates] = hex;
        }
    }
}