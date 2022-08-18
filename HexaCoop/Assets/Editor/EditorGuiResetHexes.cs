using UnityEngine;
using UnityEditor;
using System.Linq;
using System;

public class EditorGuiResetHexes : UnityEditor.EditorWindow
{

    [MenuItem("Editor/Reset Hexes")]
    static void Init()
    {
        EditorGuiResetHexes window = (EditorGuiResetHexes)EditorWindow.GetWindow(typeof(EditorGuiResetHexes), true, "Reset");
        window.Show();
    }

    void OnGUI()
    {       
        if (GUILayout.Button("Reset surfaces to plain"))
        {
            this.ResetAllHexesToPlain();
        }
        if (GUILayout.Button("Remove all Monsters & Structures"))
        {
            this.RemoveAllMonstersAndStructures();
        }  
    }

    private void ResetAllHexesToPlain()
    {
        var allHexes = GameObject.FindObjectsOfType<Hex>().ToList();

        allHexes.ForEach(hex =>
        {
            hex.HexSurfaceType = HexSurfaceType.Simple_Plain;
            HexEditorUtil.HexSurfaceTypeChanged(hex, hex.HexSurfaceType);
        });
    }

    private void RemoveAllMonstersAndStructures()
    {
        var allHexes = GameObject.FindObjectsOfType<Hex>().ToList();

        allHexes.ForEach(hex =>
        {
            hex.HexStructureType = HexStructureType.None;
            hex.HexObjectOnTileType = HexObjectOnTileType.None;
            HexEditorUtil.HexStructureTypeChanged(hex, hex.HexStructureType); // dit verwijdert ook de monsters....
        });
    }
}