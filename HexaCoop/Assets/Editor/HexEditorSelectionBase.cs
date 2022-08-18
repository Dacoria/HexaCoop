
using System.Collections;
using Unity.EditorCoroutines.Editor;
using UnityEditor;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

[CustomEditor(typeof(GameObject)), CanEditMultipleObjects]
public class HexEditorSelectionBase : Editor
{
    

    void OnSceneGUI()
    {
        if (!EditorGUILayoutToggle.UseEditorGuiScripts)
        {
            return;
        }

        if (Application.isPlaying)
        {
            return;
        }

        if (Event.current.type == EventType.MouseDown)
        {
            HandleMouseDownEvent();
        }
        else if(Event.current.type == EventType.KeyUp || Event.current.type == EventType.KeyDown)
        {
            HandleKeyUpDownEvent();
        }
    }

    private void HandleMouseDownEvent()
    {
        if (Event.current.button == 0)
        { 
            var clickedGo = HandleUtility.PickGameObject(Event.current.mousePosition, true);
            EditorCoroutineUtility.StartCoroutineOwnerless(ProcessMouseClickAfterXSeconds(0.05f, clickedGo));
        }
    }

    private IEnumerator ProcessMouseClickAfterXSeconds(float seconds, GameObject clickedGo)
    {
        yield return new EditorWaitForSeconds(seconds);

        var relatedHexFromClick = GetHexGoFromChild(clickedGo);
        if (relatedHexFromClick != null)
        {
            EditorCoroutineUtility.StartCoroutineOwnerless(SelectGoAfterXSeconds(relatedHexFromClick, 0.1f));
        }
    }

    private bool leftCntrPressed;


    private static List<GameObject> previousHexesSelected;

    private IEnumerator SelectGoAfterXSeconds(GameObject goToSelect, float seconds)
    {
        yield return new EditorWaitForSeconds(seconds);
        if (!leftCntrPressed)
        {
            SelectOneTile(goToSelect);
        }
        else
        {
            SelectMultipleTiles(goToSelect);
        }
    }

    private static void SelectMultipleTiles(GameObject goToSelect)
    {
        var selectedGoList = new List<GameObject> { goToSelect };
        var newGoList = previousHexesSelected != null ? previousHexesSelected.Concat(selectedGoList).ToList() : selectedGoList;

        previousHexesSelected = newGoList;
        Selection.objects = newGoList.ToArray();
    }

    private static void SelectOneTile(GameObject goToSelect)
    {
        var selectedGoList = new List<GameObject> { goToSelect };
        Selection.activeGameObject = goToSelect;
        Selection.objects = selectedGoList.ToArray();
        previousHexesSelected = selectedGoList;
    }

    private void HandleKeyUpDownEvent()
    {
        Event e = Event.current;
        if (e.type == EventType.KeyDown)
        {
            if (e.keyCode == KeyCode.LeftControl)
            {
                leftCntrPressed = true;
            }
        }
        else if (e.type == EventType.KeyUp)
        {
            if (e.keyCode == KeyCode.LeftControl)
            {
                leftCntrPressed = false;
            }
        }        
    }

    private GameObject GetHexGoFromChild(GameObject pickedObject)
    {
        for (var counter = 0;
            counter < 3 &&
            pickedObject?.transform?.parent != null;
            counter++)
        {
            if (pickedObject.GetComponent<Hex>() != null)
            {
                return pickedObject;
            }

            pickedObject = pickedObject.transform.parent.gameObject;            
        }

        return null;
    }
}

