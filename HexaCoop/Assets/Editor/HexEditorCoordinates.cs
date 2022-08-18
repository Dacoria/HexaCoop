using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HexCoordinates))]
public class HexEditorCoordinates : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (Application.isPlaying)
        {
            return;
        }

        var hexCoordinates = (HexCoordinates)target;
        hexCoordinates.OffSetCoordinates = hexCoordinates.transform.position.ConvertPositionToCoordinates();
    }
}