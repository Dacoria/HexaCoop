public static class Textt
{
    // networkhelper nodig voor sync rpc (aangezien deze klasse static is)

    public static void GameLocal(string text, PlayerScript playerFilter = null)
    {
        NetworkHelper.instance.SetGameText(text, playerFilter);
    }   
}