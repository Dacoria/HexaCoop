using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static partial class Rsc
{
    private static List<string> materialTileList = new List<string>
    {
        Statics.RESOURCE_PATH_MATERIAL_TILE_TYPES
    };

    private static Dictionary<string, Material> __materialTileMap;
    public static Dictionary<string, Material> MaterialTileMap
    {
        get
        {
            if (__materialTileMap == null)
                __materialTileMap = RscHelper.CreateMaterialDict(materialTileList);
            
            return __materialTileMap;
        }
    }

    private static List<string> materialColorList = new List<string>
    {
        Statics.RESOURCE_PATH_MATERIAL_COLORS,
    };

    private static Dictionary<string, Material> __materialColorMap;
    public static Dictionary<string, Material> MaterialColorMap
    {
        get
        {
            if (__materialColorMap == null)
                __materialColorMap = RscHelper.CreateMaterialDict(materialColorList);

            return __materialColorMap;
        }
    }

    public static Material GetMaterial(this HighlightColorType colorType) =>
        MaterialColorMap.Single(x => x.Key == colorType.ToString()).Value;

}