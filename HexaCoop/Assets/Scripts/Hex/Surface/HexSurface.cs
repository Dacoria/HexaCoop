
public static class HexSurface
{
    public static bool IsObstacle(this HexSurfaceType surfaceType) => surfaceType switch
        {
            HexSurfaceType.Water_Deep => true,
            HexSurfaceType.Water_Ice_Cracked => true,
            HexSurfaceType.Simple_Water => true,

            _ => false
        };

}