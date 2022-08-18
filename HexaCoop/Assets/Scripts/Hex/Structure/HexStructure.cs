
public static class HexStructure
{
    public static bool IsObstacle(this HexStructureType structureType) => structureType switch
    {
        HexStructureType.Mountain => true,
        _ => false
    };
}