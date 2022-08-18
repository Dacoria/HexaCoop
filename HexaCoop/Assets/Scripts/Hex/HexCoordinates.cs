using System;
using UnityEngine;

public class HexCoordinates : MonoBehaviour
{
    public Vector3Int OffSetCoordinates;

    private void Awake()
    {
        OffSetCoordinates = transform.position.ConvertPositionToCoordinates();
    }    
}