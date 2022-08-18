using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    private Camera mainCamera;
    public static CameraShake instance;

    private void Awake()
    {
        instance = this;
        mainCamera = Camera.main;
    }

    IEnumerator ShakeCamera(float shakeDuration, float shakeAmount, float shakeDecreaseFactor)
    {
        var originalPos = mainCamera.transform.localPosition;
        var duration = shakeDuration;
        while (duration > 0)
        {
            mainCamera.transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
            duration -= Time.deltaTime * shakeDecreaseFactor;
            yield return null;
        }
        mainCamera.transform.localPosition = originalPos;
    }
    public void Shake(float shakeDuration = 0.4f, float shakeAmount = 1.5f, float shakeDecreaseFactor = 2f)
    {
        StartCoroutine(ShakeCamera(shakeDuration, shakeAmount, shakeDecreaseFactor));
    }
}