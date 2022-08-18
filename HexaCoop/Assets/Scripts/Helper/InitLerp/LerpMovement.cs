using System;
using System.Collections;
using UnityEngine;

public class LerpMovement : MonoBehaviour
{
    public void MoveToDestination(Vector3 endPosition, float duration, Vector3? startPosition = null, float delayedStart = 0, Action callbackOnFinished = null, bool destroyOnFinished = false)
    {
        StopAllCoroutines();
        StartCoroutine(MoveToDestinationLerp(endPosition, duration, startPosition, delayedStart, callbackOnFinished, destroyOnFinished));
    }

    private IEnumerator MoveToDestinationLerp(Vector3 endPosition, float duration, Vector3? startPosition, float delayedStart, Action callbackOnFinished, bool destroyOnFinished)
    {
        var startPos = startPosition.HasValue ? startPosition.Value : transform.position;
        transform.position = startPos;

        yield return new WaitForSeconds(delayedStart);        

        float elapsedTime = 0f;
        var curve = MonoHelper.instance.CurveGradual;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float percComplete = elapsedTime / duration;
            transform.position = Vector3.Lerp(startPos, endPosition, curve.Evaluate(percComplete));
            yield return null;
        }

        callbackOnFinished?.Invoke();
        if(destroyOnFinished) { Destroy(this); }
    }
}
