public static class Textt
{
    public static void GameLocal(string text, PlayerScript playerFilter = null)
    {
        if (playerFilter == null || playerFilter.IsOnMyNetwork())
        {
            GameDialogScript.instance.AddText(text);
        }
    }

    public static void Reset() => GameDialogScript.instance.Reset();
}