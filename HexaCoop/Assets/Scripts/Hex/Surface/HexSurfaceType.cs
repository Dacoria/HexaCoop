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
    Dirt_Stones,
    Flowers,
    Granite_Stone,
    Grass,
    Grey_Stone,
    Ice,
    Lave_Stones,
    Light_Grey_Stone,
    Magma,
    Poison,
    Purple_Cracks,
    Purple_Crystal,
    Sand_Dirt,
    Sand_Stones,
    Snow,
    Snow_Rocks,
    Water_Deep,
    Water_Ice_Cracked,
    Yellow_Stone,

    Green_Triangles,
    Mini_Hexagons,
    SandRock,
    PathRocks,
    Water_Light,
    Grass2,
    Blue_3D_Blocks,
    Rotation
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