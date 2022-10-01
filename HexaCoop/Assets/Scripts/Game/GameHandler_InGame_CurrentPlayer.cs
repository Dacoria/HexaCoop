public partial class GameHandler : HexaEventCallback
{
    private PlayerScript _currentPlayer;
    public void SetCurrentPlayer(PlayerScript player) => _currentPlayer = player;
    public PlayerScript GetCurrentPlayer() => _currentPlayer;
}
