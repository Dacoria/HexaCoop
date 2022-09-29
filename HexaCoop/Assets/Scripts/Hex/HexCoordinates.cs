using UnityEngine;

public class HexCoordinates : MonoBehaviour
{
    public Vector3Int OffSetCoordinates;

    private void Awake()
    {
        SetOffSetCoordinates();
    }

    public void SetOffSetCoordinates()
    {
        OffSetCoordinates = transform.position.ConvertPositionToCoordinates();
    }
}