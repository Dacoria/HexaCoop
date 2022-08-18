using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEditor.SceneManagement;

public class EditorGuiAddRowColHex : UnityEditor.EditorWindow
{

    [MenuItem("Editor/Add_or_Remove Row or Col Hexes")]
    static void Init()
    {
        EditorGuiAddRowColHex window = (EditorGuiAddRowColHex)EditorWindow.GetWindow(typeof(EditorGuiAddRowColHex), true, "Add Row/Col");
        window.Show();
    }

    void OnGUI()
    {
        if (GUILayout.Button("Add new Hex Row"))
        {
            this.AddHexRow();
        }
        if (GUILayout.Button("Add new Hex Col"))
        {
            this.AddHexCol();
        }
        if (GUILayout.Button("Remove Top Hex Row"))
        {
            bool decision = EditorUtility.DisplayDialog(
              "Destroy all hexes top row", // title
              "Are you sure want to destroy the top row hexes?", // description
              "Yes", // OK button
              "No" // Cancel button
            );

            if (decision)
            {
                this.RemoveTopHexRow();
            }            
        }

        if (GUILayout.Button("Remove Right Hex Col"))
        {
            bool decision = EditorUtility.DisplayDialog(
              "Destroy all hexes top row", // title
              "Are you sure want to destroy the right col hexes?", // description
              "Yes", // OK button
              "No" // Cancel button
            );

            if (decision)
            {
                this.RemoveRightHexCol();
            }
        }

    }

    private void AddHexRow()
    {
        var hexRightUpperCorner = HexEditorUtil.GetHexRightUpperCorner();
        var hexRightUpperCornerCoordinate = hexRightUpperCorner.transform.position.ConvertPositionToCoordinates();

        var startCol = (hexRightUpperCornerCoordinate.z + 1) % 2; // oneven getallen beginnen met 1 als x-coordinate (anders -positie V3)
        for (int col = startCol; col <= hexRightUpperCornerCoordinate.x; col++)
        {
            var newPos = new Vector3Int(col, hexRightUpperCornerCoordinate.y, hexRightUpperCornerCoordinate.z + 1).ConvertCoordinatesToPosition();
            CreateNewHex(hexRightUpperCorner, newPos);
        }
    }

    private void AddHexCol()
    {
        var hexRightUpperCorner = HexEditorUtil.GetHexRightUpperCorner();
        var hexRightUpperCornerCoordinate = hexRightUpperCorner.transform.position.ConvertPositionToCoordinates();

        for (int row = 0; row <= hexRightUpperCornerCoordinate.z; row++)
        {
            var newPos = new Vector3Int(hexRightUpperCornerCoordinate.x + 1, hexRightUpperCornerCoordinate.y, row).ConvertCoordinatesToPosition();
            CreateNewHex(hexRightUpperCorner, newPos);
        }
    }

    private void RemoveTopHexRow()
    {
        var hexRightUpperCorner = HexEditorUtil.GetHexRightUpperCorner();
        var hexRightUpperCornerCoordinate = hexRightUpperCorner.transform.position.ConvertPositionToCoordinates();

        var currentHexes = GameObject.FindObjectsOfType<Hex>();
        var upperRowHexes = currentHexes.Where(x => x.transform.position.ConvertPositionToCoordinates().z == hexRightUpperCornerCoordinate.z).ToList();

        for (int i = upperRowHexes.Count - 1; i >= 0; i--)
        {
            DestroyImmediate(upperRowHexes[i].gameObject);
        }

        EditorSceneManager.MarkSceneDirty(HexEditorUtil.GetHexRightUpperCorner().gameObject.scene);
    }

    private void RemoveRightHexCol()
    {
        var hexRightUpperCorner = HexEditorUtil.GetHexRightUpperCorner();
        var hexRightUpperCornerCoordinate = hexRightUpperCorner.transform.position.ConvertPositionToCoordinates();

        var currentHexes = GameObject.FindObjectsOfType<Hex>();
        var leftColHexes = currentHexes.Where(x => x.transform.position.ConvertPositionToCoordinates().x == hexRightUpperCornerCoordinate.x).ToList();

        for (int i = leftColHexes.Count - 1; i >= 0; i--)
        {
            DestroyImmediate(leftColHexes[i].gameObject);
        }

        EditorSceneManager.MarkSceneDirty(HexEditorUtil.GetHexRightUpperCorner().gameObject.scene);
    }

    private static void CreateNewHex(Hex hexRightUpperCorner, Vector3 newPosition)
    {
        var prefabRoot = PrefabUtility.GetCorrespondingObjectFromSource(hexRightUpperCorner);
        var newHex = PrefabUtility.InstantiatePrefab(prefabRoot, hexRightUpperCorner.transform.parent) as Hex;

        newHex.transform.position = newPosition;

        newHex.HexSurfaceType = HexSurfaceType.Simple_Plain;
        newHex.HexStructureType = HexStructureType.None;
        newHex.HexObjectOnTileType = HexObjectOnTileType.None;

        var prefabForestRoot = PrefabUtility.GetCorrespondingObjectFromSource(GetForestGoFromHex(hexRightUpperCorner));
        var surfaceGo = PrefabUtility.InstantiatePrefab(prefabForestRoot, Utils.GetChildGoByName(newHex.gameObject, "Main").transform) as GameObject;

        if (Rsc.MaterialTileMap.TryGetValue(newHex.HexSurfaceType.ToString(), out Material result))
        {
            var meshRenderer = surfaceGo.GetComponent<MeshRenderer>();
            meshRenderer.material = result;
        }

        var props = GetPropsFromHex(newHex);
        props.transform.position = props.transform.position + new Vector3(0, 1, 0);

        EditorUtility.SetDirty(newHex);
        EditorSceneManager.MarkSceneDirty(newHex.gameObject.scene);
    }

    private static GameObject GetForestGoFromHex(Hex hex)
    {
        var main = Utils.GetChildGoByName(hex.gameObject, "Main");
        return Utils.GetChildGoByName(main.gameObject, "hex_forest", containMatch: true);
    }

    private static GameObject GetPropsFromHex(Hex hex) => Utils.GetChildGoByName(hex.gameObject, "Props");

}