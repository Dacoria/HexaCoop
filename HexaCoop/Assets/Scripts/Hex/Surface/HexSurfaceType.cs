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
    Dirt_Leave_Stones,
    Flowers,
    Grass,
    Ice,
    Lave_Stones,
    Light_Grey_Stone,
    Magma,
    Poison,
    Purple_Cracks,
    Purple_Crystal,
    Sand_Dirt,
    Snow,
    Snow_Rocks,
    Water_Deep,
    Water_Ice_Cracked,
    Yellow_Stone,

    SandRock,
    Water_Light,
    Grass2,
    Blue_3D_Blocks,
}

public static class HexSurfaceExt
{
    public static bool IsObstacle(this HexSurfaceType surfaceType) => surfaceType switch
    {
        HexSurfaceType.Water_Deep => true,
        HexSurfaceType.Water_Ice_Cracked => true,
        HexSurfaceType.Simple_Water => true,

        _ => false
    };
}