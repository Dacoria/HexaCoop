using UnityEngine;

public interface IObjectOnTile
{
    public Hex CurrentHexTile { get; }
    public void SetCurrentHexTile(Hex hex);
    public GameObject GameObject { get; }
}