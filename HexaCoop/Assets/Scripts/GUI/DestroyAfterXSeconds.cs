using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DestroyAfterXSeconds : MonoBehaviour
{
    public float SecondsTillDestroy = 3;

    private void Start()
    {
        StartCoroutine(DestroyGo());
    }

    private IEnumerator DestroyGo()
    {
        yield return Wait4Seconds.Get(SecondsTillDestroy);
        Destroy(gameObject);
    }
}
