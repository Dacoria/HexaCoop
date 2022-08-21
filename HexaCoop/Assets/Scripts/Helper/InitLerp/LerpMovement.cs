using System;
using System.Collections;
using UnityEngine;

public class LerpMovement : MonoBehaviour
{
    public void AppearFromDown(float distance, float duration, float delayedStart = 0, Action callbackOnFinished = null, bool destroyGoOnFinished = false, Animator animator = null)
    {
        StopAllCoroutines();
        var startPos = transform.position + Vector3.down * distance;
        var endPos = transform.position;
        StartCoroutine(MoveToDestinationLerp(endPos, duration, startPos, delayedStart, callbackOnFinished, destroyGoOnFinished, animator));
    }

    public void MoveDown(float distance, float duration, Vector3? startPosition = null, float delayedStart = 0, Action callbackOnFinished = null, bool destroyGoOnFinished = false, Animator animator = null)
    {
        StopAllCoroutines();
        var startPos = startPosition.HasValue ? startPosition.Value : transform.position;
        StartCoroutine(MoveToDestinationLerp(startPos + Vector3.down * distance, duration, startPos, delayedStart, callbackOnFinished, destroyGoOnFinished, animator));
    }

    public void MoveToDestination(Vector3 endPosition, float duration, Vector3? startPosition = null, float delayedStart = 0, Action callbackOnFinished = null, bool destroyGoOnFinished = false, Animator animator = null)
    {
        StopAllCoroutines();
        StartCoroutine(MoveToDestinationLerp(endPosition, duration, startPosition, delayedStart, callbackOnFinished, destroyGoOnFinished, animator));
    }

    private IEnumerator MoveToDestinationLerp(Vector3 endPosition, float duration, Vector3? startPosition, float delayedStart, Action callbackOnFinished, bool destroyGoOnFinished, Animator animator)
    {
        var startPos = startPosition.HasValue ? startPosition.Value : transform.position;
        transform.position = startPos;

        yield return Wait4Seconds.Get(delayedStart);

        animator?.SetBool(Statics.ANIMATION_BOOL_RUN, true);

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
        animator?.SetBool(Statics.ANIMATION_BOOL_RUN, false);
        if (destroyGoOnFinished) { Destroy(gameObject); }
        
    }
}
