// Komt overeen met namen van de shaders -> vereist!

public enum HexSurfaceType
{
    Simple_Plain,
    Simple_Rock,
    Simple_Sand,
    Simple_Water,
    Big_Brown_Stones,
    Bricks,
    Desert_Sand,
    Flowers,
    Grass,
    Ice,
    Ice_Dark,
    Lava_Stones,
    Lava_Stones_Remains,
    Light_Grey_Stone,
    Magma,
    Poison,
    Purple_Cracks,
    Purple_Crystal,
    Sand_Dirt,
    Snow,
    Snow_Rocks,
    Water_Deep,
    Water_Moving,
    Water_Ice_Cracked,
    Yellow_Stone,

    SandRock,
    Water_Light,
    Blue_3D_Blocks,
}

public static class HexSurfaceExt
{
    public static bool IsObstacle(this HexSurfaceType surfaceType)
    {
        return surfaceType.IsWater();
    }

    public static bool IsWater(this HexSurfaceType surfaceType) => surfaceType switch
    {
        HexSurfaceType.Water_Deep => true,
        HexSurfaceType.Water_Moving => true,
        HexSurfaceType.Water_Ice_Cracked => true,
        HexSurfaceType.Water_Light => true,
        HexSurfaceType.Simple_Water => true,

        _ => false
    };
}