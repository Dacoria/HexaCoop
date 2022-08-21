
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEditor.SceneManagement;

public static class HexEditorUtil
{
    public static void HexSurfaceTypeChanged(Hex hex, HexSurfaceType to)
    {
        var mainGo = GetMainGo(hex);
        if(mainGo == null || mainGo.transform.childCount == 0)
        {
            return;
        }    

        var getGoSurfaceBase = mainGo.transform.GetChild(0);

        if (Rsc.MaterialTileMap.TryGetValue(to.ToString(), out Material result))
        {
            var meshRenderer = getGoSurfaceBase.GetComponent<MeshRenderer>();
            meshRenderer.material = result;
        }

        EditorUtility.SetDirty(hex);
        EditorSceneManager.MarkSceneDirty(hex.gameObject.scene);
    }

    public static void HexStructureTypeChanged(Hex hex, HexStructureType to)
    {
        var structureGo = GetStructuresGo(hex);
        DestroyChildrenOfGo(structureGo);
        if(HasHexTypeStructures(to))
        {
            if (Rsc.GoStructureMap.TryGetValue(to.ToString() + "Structure", out GameObject result))
            {
                var go = PrefabUtility.InstantiatePrefab(result, structureGo.transform) as GameObject;
                go.transform.rotation = new Quaternion(0, 180, 0, 0);
            }
        }

        EditorUtility.SetDirty(hex);
        EditorSceneManager.MarkSceneDirty(hex.gameObject.scene);
    }

    public static void HexObjectOnTileTypeChanged(Hex hex, HexObjectOnTileType to)
    {
        var structureGo = GetStructuresGo(hex);
        DestroyChildrenOfGo(structureGo);
        if(to != HexObjectOnTileType.None)
        {
            if (Rsc.GoEnemiesOrObjMap.TryGetValue(to.ToString(), out GameObject result))
            {
                var go = PrefabUtility.InstantiatePrefab(result, structureGo.transform) as GameObject;
                go.transform.rotation = new Quaternion(0, 180, 0, 0);
                var enemyScript = go.GetComponent<EnemyScript>();
                if(enemyScript != null)
                {
                    enemyScript.SetCurrentHexTile(hex);
                }
            }
        }

        EditorUtility.SetDirty(hex);
        EditorSceneManager.MarkSceneDirty(hex.gameObject.scene);
    }

    public static void DestroyChildrenOfGo(GameObject structuresGo)
    {
        for (int i = structuresGo.transform.childCount - 1; i >= 0; i--)
        {
            var child = structuresGo.transform.GetChild(i);
            Object.DestroyImmediate(child.gameObject);
        }
    }

    private static bool HasHexTypeStructures(HexStructureType type)
    {
        switch(type)
        {
            case HexStructureType.Castle:
            case HexStructureType.Well:
            case HexStructureType.Hill:
            case HexStructureType.Market:
            case HexStructureType.Forest:
            case HexStructureType.Mountain:
            case HexStructureType.Crystal:
            case HexStructureType.Portal:
                //case HexStructureType.GoldCoin:
                //case HexStructureType.BottleBlue:
                //case HexStructureType.BottleGreen:
                //case HexStructureType.BottleRed:
                //case HexStructureType.Coin:
                //case HexStructureType.Box:
                return true;
            default:
                return false;
        }
    }


    private static GameObject GetMainGo(Hex hex) => Utils.GetChildGoByName(hex.gameObject, "Main");
    private static GameObject GetStructuresGo(Hex hex) => Utils.GetChildGoByName(hex.gameObject, "Props");

    public static Hex GetHexRightUpperCorner()
    {
        var currentHexes = GameObject.FindObjectsOfType<Hex>();
        var hexRightUpperCornerCoordinate = currentHexes.Select(x => x.transform.position.ConvertPositionToCoordinates())
            .OrderByDescending(z => z.z)
            .ThenByDescending(x => x.x)
            .First();
        var hexRightUpperCorner = currentHexes.First(x => x.transform.position.ConvertPositionToCoordinates() == hexRightUpperCornerCoordinate);

        return hexRightUpperCorner;
    }

    public static void ResetHexToDefault(Hex hex)
    {
        hex.HexSurfaceType = HexSurfaceType.Simple_Plain;
        HexSurfaceTypeChanged(hex, hex.HexSurfaceType);

        hex.HexStructureType = HexStructureType.None;
        hex.HexObjectOnTileType = HexObjectOnTileType.None;
        HexStructureTypeChanged(hex, hex.HexStructureType); // dit verwijdert ook de monsters....

        EditorUtility.SetDirty(hex);
        EditorSceneManager.MarkSceneDirty(hex.gameObject.scene);
    }
}

