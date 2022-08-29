public static class EndPlayerTurnEventInvoker
{    
    public static void EndPlayerTurn(PlayerScript player)
    {
        if(Settings.UseQueueAbilities)
        {
            NetworkAE.instance.Invoker_EndPlayerTurnWithQueue(player);
        }
        else
        {
            NetworkAE.instance.Invoker_EndPlayerTurn(player);
        }
    }

    public static void EndTurn(this PlayerScript player) => EndPlayerTurn(player);
}