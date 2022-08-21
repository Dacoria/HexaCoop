
public enum HexStructureType
{
    None,
    Forest,
    Hill,
    Market,
    Castle,
    Well,
    Mountain,
    Crystal,
    Portal,
    //BottleRed,
    //BottleGreen,
    //BottleBlue,
    //GoldCoin,
    //Coin,
    //Box
}

public static class HexStructureExt
{
    public static bool HasStructure(this HexStructureType type)
    {
        switch (type)
        {
            case HexStructureType.None:
                return false;
            default:
                return true;
        }
    }

    public static bool IsObstacle(this HexStructureType structureType) => structureType switch
    {
        HexStructureType.Mountain => true,
        _ => false
    };
}