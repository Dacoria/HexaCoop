using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RotationBehaviour : MonoBehaviour
{
    public float accelx = 100;

    void FixedUpdate()
    {
        transform.Rotate(accelx * Time.fixedDeltaTime, 0, 0);
    }
}
