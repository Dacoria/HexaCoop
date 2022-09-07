using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

public class ScrollTexture : MonoBehaviour
{
    private float scrollX = 0.08f;
    private float scrollY = -0.05f;

    private void Update()
    {
        var offSetX = Time.time * scrollX;
        var offSetY = Time.time * scrollY;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2((float)offSetX, (float)offSetY);
    }
}