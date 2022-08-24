public partial class GameHandler : HexaEventCallback
{
    protected override void OnEndRound(bool reachedMiddle, PlayerScript pWinner)
    {
        GameStatus = GameStatus.RoundEnded;
        Textt.GameLocal("Game has ended! " + pWinner.PlayerName + " wins!!!");
    }

    protected override void OnEndGame()
    {
        GameStatus = GameStatus.GameEnded;
    }

    public void CheckEndOfRound()
    {
        if(NetworkHelper.instance.GetAllPlayers(isAlive: true).Count == 1)
        {
            NetworkAE.instance.RoundEnded(achievedTarget: false, NetworkHelper.instance.GetAllPlayers(isAlive: true)[0]);
        }
    }
}