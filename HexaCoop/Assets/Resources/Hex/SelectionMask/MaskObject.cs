using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskObject : MonoBehaviour
{
    void OnEnable()
    {
        GetComponent<MeshRenderer>().material.renderQueue = 3002;
    }
}
