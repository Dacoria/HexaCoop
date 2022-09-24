public interface IUnit: IObjectOnTile
{
    public int Id { get; }
    public UnitType UnitType { get; }
    public bool IsAlive { get; }
    public void MoveToNewDestination(Hex tile);
}