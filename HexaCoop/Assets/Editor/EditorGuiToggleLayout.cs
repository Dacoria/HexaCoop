using UnityEngine;
using UnityEditor;

public class EditorGUILayoutToggle : UnityEditor.EditorWindow
{
    public static bool UseEditorGuiScripts = true;

    [MenuItem("Editor/GUILayout Toggle Usage")]
    static void Init()
    {        
        EditorGUILayoutToggle window = (EditorGUILayoutToggle)EditorWindow.GetWindow(typeof(EditorGUILayoutToggle), true, "Window");
        window.Show();
    }

    void OnGUI()
    {
        UseEditorGuiScripts = EditorGUILayout.Toggle("Toggle Editor", UseEditorGuiScripts);
        if (GUILayout.Button("Close"))
            this.Close();

    }
}