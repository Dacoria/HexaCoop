
public enum HexObjectOnTileType
{
    None,
    Bat,
    Boximon,
    EarthElemental,
    FireElemental,
    IceElemental,
    RockGolem,
    DogKnight,
    Slime,
    Turtle,
    Skeleton,
    Mummy,
    Bombie,
    BearTrap,
    Artillery_Pickup,
    MeteorStrike_Pickup,

    //BottleRed,
    //BottleGreen,
    //BottleBlue,
    //GoldCoin,
    //Coin,
    //Box,
    //Crystal
}

public static class HexObjectOnTileExt
{
    public static bool IsPickup(this HexObjectOnTileType type) => type switch
    {
        HexObjectOnTileType.Artillery_Pickup => true,
        HexObjectOnTileType.MeteorStrike_Pickup => true,
        _ => false
    };
}