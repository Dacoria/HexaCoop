public enum HexStartPlayerType
{
    None,
    Player1,
    Player2,
    Player3,
    Player4
}

public static class HexStartPlayerExt
{
    public static string GetText(this HexStartPlayerType hexStartPlayer) => hexStartPlayer switch
    {
        HexStartPlayerType.Player1 => "P1",
        HexStartPlayerType.Player2 => "P2",
        HexStartPlayerType.Player3 => "P3",
        HexStartPlayerType.Player4 => "P4",
        _ => throw new System.NotImplementedException()
    };

    public static int GetIndex(this HexStartPlayerType hexStartPlayer) => hexStartPlayer switch
    {
        HexStartPlayerType.Player1 => 0,
        HexStartPlayerType.Player2 => 1,
        HexStartPlayerType.Player3 => 2,
        HexStartPlayerType.Player4 => 3,
        _ => throw new System.NotImplementedException()
    };
}