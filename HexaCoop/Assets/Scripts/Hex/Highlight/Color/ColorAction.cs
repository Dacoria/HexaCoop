public static class ColorAction
{
    public static HighlightColorType GetColor(this HighlightActionType actionType) => actionType switch
    {
        HighlightActionType.SelectTile => HighlightColorType.White,
        HighlightActionType.VisionOption => HighlightColorType.Yellow,
        HighlightActionType.RadarOption => HighlightColorType.Blue,
        HighlightActionType.MoveOption => HighlightColorType.LightBlue,
        HighlightActionType.RocketHit => HighlightColorType.Red,
        HighlightActionType.EnemyOption => HighlightColorType.Orange,
        _ => throw new System.Exception("HighlightActionType " + actionType + " is not supported")
    };
}

public enum HighlightActionType
{
    SelectTile,
    MoveOption,
    RadarOption,
    VisionOption,
    RocketHit,
    EnemyOption
}
