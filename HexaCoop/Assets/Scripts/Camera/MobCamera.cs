
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DigitalRubyShared;

public class MobCamera : MonoBehaviour
{
    private TapGestureRecognizer tapGesture;
    private SwipeGestureRecognizer swipeGesture;
    private PanGestureRecognizer panGesture;
    private ScaleGestureRecognizer scaleGesture;
    private RotateGestureRecognizer rotateGesture;
    private LongPressGestureRecognizer longPressGesture;

    private readonly List<Vector3> swipeLines = new List<Vector3>();

    private void DebugText(string text, params object[] format)
    {
        //Debug.Log(string.Format(text, format));
    }
    
    private void PanGestureCallback(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Executing)
        {
            DebugText("Panned, Location: {0}, {1}, Delta: {2}, {3}", gesture.FocusX, gesture.FocusY, gesture.DeltaX, gesture.DeltaY);

            float deltaX = panGesture.DeltaX / 25.0f;
            float deltaY = panGesture.DeltaY / 25.0f;

            Vector3 pos = Camera.main.transform.position;
            pos.x -= deltaX;
            pos.z -= deltaY;
            Camera.main.transform.position = pos;
        }
    }

    private void CreatePanGesture()
    {
        panGesture = new PanGestureRecognizer();
        panGesture.MinimumNumberOfTouchesToTrack = 2;
        panGesture.StateUpdated += PanGestureCallback;
        FingersScript.Instance.AddGesture(panGesture);
    }

    private void ScaleGestureCallback(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Executing)
        {
            DebugText("Scaled: {0}, Focus: {1}, {2}", scaleGesture.ScaleMultiplier, scaleGesture.FocusX, scaleGesture.FocusY);
            Camera.main.transform.localScale *= scaleGesture.ScaleMultiplier;
        }
    }

    private void CreateScaleGesture()
    {
        scaleGesture = new ScaleGestureRecognizer();
        scaleGesture.StateUpdated += ScaleGestureCallback;
        FingersScript.Instance.AddGesture(scaleGesture);
    }

    private void RotateGestureCallback(GestureRecognizer gesture)
    {
        if (gesture.State == GestureRecognizerState.Executing)
        {
            Camera.main.transform.Rotate(0.0f, 0.0f, rotateGesture.RotationRadiansDelta * Mathf.Rad2Deg);
        }
    }

    private void CreateRotateGesture()
    {
        rotateGesture = new RotateGestureRecognizer();
        rotateGesture.StateUpdated += RotateGestureCallback;
        FingersScript.Instance.AddGesture(rotateGesture);
    }  

    private static bool? CaptureGestureHandler(GameObject obj)
    {
        // I've named objects PassThrough* if the gesture should pass through and NoPass* if the gesture should be gobbled up, everything else gets default behavior
        if (obj.name.StartsWith("PassThrough"))
        {
            // allow the pass through for any element named "PassThrough*"
            return false;
        }
        else if (obj.name.StartsWith("NoPass"))
        {
            // prevent the gesture from passing through, this is done on some of the buttons and the bottom text so that only
            // the triple tap gesture can tap on it
            return true;
        }

        // fall-back to default behavior for anything else
        return null;
    }

    private void Start()
    {
        // don't reorder the creation of these :)
        CreatePanGesture();
        CreateScaleGesture();
        CreateRotateGesture();

        // pan, scale and rotate can all happen simultaneously
        panGesture.AllowSimultaneousExecution(scaleGesture);
        panGesture.AllowSimultaneousExecution(rotateGesture);
        scaleGesture.AllowSimultaneousExecution(rotateGesture);

        // prevent the one special no-pass button from passing through,
        //  even though the parent scroll view allows pass through (see FingerScript.PassThroughObjects)
        FingersScript.Instance.CaptureGestureHandler = CaptureGestureHandler;
    }   
}
