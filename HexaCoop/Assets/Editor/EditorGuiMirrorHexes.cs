using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;
using UnityEditor.SceneManagement;

public class EditorGuiMirrorHexes : UnityEditor.EditorWindow
{

    [MenuItem("Editor/Mirror Hexes")]
    static void Init()
    {
        EditorGuiMirrorHexes window = (EditorGuiMirrorHexes)EditorWindow.GetWindow(typeof(EditorGuiMirrorHexes), true, "Mirror Hexes");
        window.Show();
    }

    void OnGUI()
    {
        if (GUILayout.Button("Mirror Hexes - 2 sides (2 quarters)"))
        {
            this.MirrorHexes(fourSides: false);
        }
        if (GUILayout.Button("Mirror Hexes - 4 sides"))
        {
            this.MirrorHexes(fourSides: true);
        }
        if (GUILayout.Button("Reset mirrorfields"))
        {
            this.ResetMirrorFields();
        }        
    }

    public enum MirrorType
    {
        OtherSide,
        LeftUpper,
        BottomDown,
    }

    private void MirrorHexes(bool fourSides)
    {
        var tileRightUpperCorner = HexEditorUtil.GetHexRightUpperCorner().transform.position.ConvertPositionToCoordinates();
        if (tileRightUpperCorner.z % 2 == 1)
        {
            var continueYN = EditorUtility.DisplayDialog("No", "Oneven numer of rows needed! The mirror won't be exact. Still continue?", "Yes", "No");
            if (!continueYN)
            {
                return;
            }
        }


        var xLimit = Mathf.Ceil((tileRightUpperCorner.x - 1) / 2f);
        var zLimit = Mathf.Ceil((tileRightUpperCorner.z - 1) / 2f);

        var hexDict = GetHexDict();
        var hasEvenNumbersOfRows = tileRightUpperCorner.z % 2 == 1;

        if (hasEvenNumbersOfRows)
        {
            for (int z = 1; z <= zLimit; z = z + 2)
            {
                var sideHexNotMirrored = new Vector3Int(tileRightUpperCorner.x, 0, tileRightUpperCorner.z - z);
                var hexNotMirrored = hexDict[sideHexNotMirrored];
                HexEditorUtil.ResetHexToDefault(hexNotMirrored);
            }
        }


        for (int x = 0; x <= xLimit; x++)
        {
            for (int z = 0; z <= zLimit; z++)
            {
                MirrorHex(MirrorType.OtherSide, x, z, tileRightUpperCorner, hexDict);
                if (fourSides)
                {
                    MirrorHex(MirrorType.LeftUpper, x, z, tileRightUpperCorner, hexDict);
                    MirrorHex(MirrorType.BottomDown, x, z, tileRightUpperCorner, hexDict);
                }
            }
        }
    }

    private void MirrorHex(MirrorType mirrorType, int x, int z, Vector3Int tileRightUpperCorner, Dictionary<Vector3Int, Hex> hexDict)
    {
        var hasEvenNumbersOfRows = tileRightUpperCorner.z % 2 == 1; // want 0, 1, 2 ==> 3 tiles

        var fromV3Int = new Vector3Int(x, 0, z);
        if (hexDict.ContainsKey(fromV3Int))
        {
            var hexFrom = hexDict[fromV3Int];
            var xboosterForToHex = (z % 2 == 1 && !hasEvenNumbersOfRows) ? 1 : 0;

            var hexToX = mirrorType.In(MirrorType.OtherSide, MirrorType.BottomDown) ? tileRightUpperCorner.x - x + xboosterForToHex : x;
            var hexToZ = mirrorType.In(MirrorType.OtherSide, MirrorType.LeftUpper) ? tileRightUpperCorner.z - z : z;

            var toV3Int = new Vector3Int(hexToX, 0, hexToZ);
            if (hexDict.ContainsKey(toV3Int))
            {
                var hexTo = hexDict[toV3Int];
                UpdateHexSettingsFromOtherHex(hexFrom, hexTo);
            }
        }
    }

    private Dictionary<Vector3Int, Hex> GetHexDict()
    {
        var res = new Dictionary<Vector3Int, Hex>();
        var hexes = GameObject.FindObjectsOfType<Hex>();
        foreach (var hex in hexes)
        {
            res.Add(hex.transform.position.ConvertPositionToCoordinates(), hex);
        }

        return res;
    }

    private void UpdateHexSettingsFromOtherHex(Hex hexToCopyFrom, Hex hexToCopyTo)
    {
        if (hexToCopyTo.HexSurfaceType != hexToCopyFrom.HexSurfaceType)
        {
            hexToCopyTo.HexSurfaceType = hexToCopyFrom.HexSurfaceType;
            HexEditorUtil.HexSurfaceTypeChanged(hexToCopyTo, hexToCopyTo.HexSurfaceType);
        }

        if (hexToCopyTo.HexStructureType != hexToCopyFrom.HexStructureType)
        {
            hexToCopyTo.HexStructureType = hexToCopyFrom.HexStructureType;
            hexToCopyTo.HexObjectOnTileType = hexToCopyFrom.HexObjectOnTileType;
            HexEditorUtil.HexStructureTypeChanged(hexToCopyTo, hexToCopyTo.HexStructureType);
        }
        else if (hexToCopyTo.HexObjectOnTileType != hexToCopyFrom.HexObjectOnTileType)
        {
            // OF obj, OF structure. Niet beide; vandaar zo
            hexToCopyTo.HexStructureType = hexToCopyFrom.HexStructureType;
            hexToCopyTo.HexObjectOnTileType = hexToCopyFrom.HexObjectOnTileType;
            HexEditorUtil.HexObjectOnTileTypeChanged(hexToCopyTo, hexToCopyTo.HexObjectOnTileType);
        }

        EditorUtility.SetDirty(hexToCopyTo);
        EditorSceneManager.MarkSceneDirty(hexToCopyTo.gameObject.scene);
    }

    private void ResetMirrorFields()
    {
        var tileRightUpperCorner = HexEditorUtil.GetHexRightUpperCorner().transform.position.ConvertPositionToCoordinates();

        var xLimit = Mathf.Ceil((tileRightUpperCorner.x - 1) / 2f);
        var zLimit = Mathf.Ceil((tileRightUpperCorner.z - 1) / 2f);

        var hexDict = GetHexDict();

        foreach (var kvp in hexDict)
        {
            if(kvp.Key.x <= xLimit && kvp.Key.z <= zLimit)
            {
                continue;
            }

            HexEditorUtil.ResetHexToDefault(kvp.Value);
        }

        EditorSceneManager.MarkSceneDirty(HexEditorUtil.GetHexRightUpperCorner().gameObject.scene);
    }
}