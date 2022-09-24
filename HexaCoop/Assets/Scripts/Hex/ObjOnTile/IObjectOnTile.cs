public interface IObjectOnTile
{
    public Hex CurrentHexTile { get; }
    public void SetCurrentHexTile(Hex hex);
}