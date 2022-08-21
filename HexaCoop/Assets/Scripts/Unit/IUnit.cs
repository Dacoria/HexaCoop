using UnityEngine;

public interface IUnit
{
    public Hex CurrentHexTile { get; }
    public void SetCurrentHexTile(Hex hex);
    public UnitType UnitType { get; }
    public bool IsAlive { get; }
    public int Id { get; }
    public void MoveToNewDestination(Hex tile);
}