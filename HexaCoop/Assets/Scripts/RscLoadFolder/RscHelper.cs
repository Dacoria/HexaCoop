using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public static class RscHelper
{
    public static List<T> TypeList<T>(string path) =>
        Resources
        .LoadAll(path, typeof(T))
        .Cast<T>()
        .ToList();

    public static Dictionary<string, GameObject> CreateGoDict(List<string> rscList)
    {
        var resultMap = new Dictionary<string, GameObject>();
        rscList.ForEach(rsc => 
            resultMap.AddRange(
                TypeList<GameObject>(rsc).ToDictionary(x => x.name, y => (GameObject)y)
        ));
        return resultMap;
    }

    public static Dictionary<string, Material> CreateMaterialDict(List<string> rscList)
    {
        var resultMap = new Dictionary<string, Material>();
        rscList.ForEach(rsc =>
            resultMap.AddRange(
                TypeList<Material>(rsc).ToDictionary(x => x.name, y => (Material)y)
        ));
        return resultMap;
    }

    public static Dictionary<string, Sprite> CreateSpriteDict(List<string> rscList)
    {
        var resultMap = new Dictionary<string, Sprite>();
        rscList.ForEach(rsc =>
            resultMap.AddRange(
                TypeList<Sprite>(rsc).ToDictionary(x => x.name, y => (Sprite)y)
        ));
        return resultMap;
    }
}