using System;
using System.Collections;
using System.Linq;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;

[InitializeOnLoadAttribute]
public static class HierarchyMonitor
{
    static HierarchyMonitor()
    {
        EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
    }

    private static bool isRunning;
    private static bool previousUseEditorGuiScripts;

    private static void OnHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        if (!EditorGUILayoutToggle.UseEditorGuiScripts)
        {
            if(previousUseEditorGuiScripts)
            {
                SetAllHexToFlag(HideFlags.None);
            }

            return;
        }

        if (Application.isPlaying)
        {
            if (previousUseEditorGuiScripts)
            {
                SetAllHexToFlag(HideFlags.None);
                previousUseEditorGuiScripts = false;
            }
            return;
        }

        if (!isRunning)
        {       
            EditorCoroutineUtility.StartCoroutineOwnerless(HierarchyWindowItemOnGUIXSeconds(0.20f));
            isRunning = true;
        }

        previousUseEditorGuiScripts = EditorGUILayoutToggle.UseEditorGuiScripts;
    }

    private static IEnumerator HierarchyWindowItemOnGUIXSeconds(float seconds)
    {
        yield return new EditorWaitForSeconds(seconds);
        isRunning = false;

        var currentSelectedItem = Selection.activeGameObject;

        if(currentSelectedItem?.GetComponent<Hex>() != null)
        {
            SetAllHexToFlag(HideFlags.HideInHierarchy);

            var selectedHex = currentSelectedItem.GetComponent<Hex>();
            SetFlagOnHexChildren(selectedHex, HideFlags.None);
        }        
    }

    private static void SetAllHexToFlag(HideFlags hideFlags)
    {
        foreach (Hex hex in Resources.FindObjectsOfTypeAll(typeof(Hex)))
        {
            SetFlagOnHexChildren(hex, hideFlags);
        }
    }

    private static void SetFlagOnHexChildren(Hex hex, HideFlags hideFlags)
    {
        for (int i = 0; i < hex.transform.childCount; i++)
        {
            var childHex = hex.transform.GetChild(i);
            childHex.hideFlags = hideFlags;
        }
    }
}