using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightSelection : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    public void SetColor(HighlightColorType color)
    {
        var colorMaterial = color.GetMaterial();
        meshRenderer.material = colorMaterial;
        meshRenderer.material.renderQueue = 3002; // voor masking
    }
}