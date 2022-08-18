using System;
using System.Collections;
using UnityEngine;

public class LerpRotation: HexaEventCallback
{
    private float previousAngleDiff = 0;

    public void RotateTowardsDestination(Vector3 endPosition, float delayedStart = 0, Action callbackOnFinished = null, bool destroyOnFinished = false)
    {
        StopAllCoroutines();
        StartCoroutine(RotateTowardsDestinationLerp(endPosition, delayedStart, callbackOnFinished, destroyOnFinished));
    }

    private IEnumerator RotateTowardsDestinationLerp(Vector3 endPosition, float delayedStart, Action callbackOnFinished, bool destroyOnFinished)
    {
        float elapsedTime = 0f;
        var targetDirection = endPosition - transform.position;

        yield return new WaitForSeconds(delayedStart);

        while (elapsedTime < 3)
        {
            elapsedTime += Time.deltaTime;
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, 4 * Time.deltaTime, 0.0f);

            var currentAngleDiff = Vector3.Angle(newDirection, targetDirection);
            if (Math.Abs(currentAngleDiff - previousAngleDiff) < 0.01)
            {
                break;
            }

            transform.rotation = Quaternion.LookRotation(newDirection);

            previousAngleDiff = currentAngleDiff;
            yield return null;
        }

        callbackOnFinished?.Invoke();
        if (destroyOnFinished) { Destroy(this); }
    }

    public void RotateTowardsAngle(Vector3 endPositionV3, float duration = 0, Vector3? startRotationV3 = null, float delayedStart = 0, Action callbackOnFinished = null, bool destroyOnFinished = false)
    {
        Quaternion? startRot = startRotationV3.HasValue ? Quaternion.Euler(startRotationV3.Value.x, startRotationV3.Value.y, startRotationV3.Value.z) : null;
        Quaternion endRot = Quaternion.Euler(endPositionV3.x, endPositionV3.y, endPositionV3.z);
        RotateTowardsAngle(endRot, duration, startRot, delayedStart, callbackOnFinished, destroyOnFinished);
    }

    public void RotateTowardsAngle(Quaternion endRotation, float duration, Quaternion? startRotation = null, float delayedStart = 0, Action callbackOnFinished = null, bool destroyOnFinished = false)
    {
        StopAllCoroutines();
        StartCoroutine(RotateTowardsAngleLerp(endRotation, duration, startRotation, delayedStart, callbackOnFinished, destroyOnFinished));
    }

    private IEnumerator RotateTowardsAngleLerp(Quaternion endRotation, float duration, Quaternion? startRotation, float delayedStart, Action callbackOnFinished, bool destroyOnFinished)
    {        
        yield return new WaitForSeconds(delayedStart);

        Quaternion startPos = startRotation.HasValue ? startRotation.Value : transform.rotation;
        transform.rotation = startPos;

        float elapsedTime = 0f;
        var curve = MonoHelper.instance.CurveGradual;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float percComplete = elapsedTime / duration;
            transform.rotation = Quaternion.Lerp(startPos, endRotation, percComplete);
            yield return null;
        }

        callbackOnFinished?.Invoke();
        if (destroyOnFinished) { Destroy(this); }
    }
}

