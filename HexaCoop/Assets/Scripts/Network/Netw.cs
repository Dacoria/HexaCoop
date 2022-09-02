using System.Collections.Generic;
using System.Linq;
public static class Netw
{
    public static PlayerScript MyPlayer() => NetworkHelper.instance.GetMyPlayer();
    public static PlayerScript CurrPlayer() => Settings.UseSimultaniousTurns ? MyPlayer() : GameHandler.instance.GetCurrentPlayer();
    public static List<PlayerScript> PlayersOnMyNetwork() => NetworkHelper.instance.GetMyPlayers();
    public static PlayerScript GetPlayer(int id) => NetworkHelper.instance.GetAllPlayers(isAlive: true).FirstOrDefault(x => x.Id == id);

    public static bool IsMe(this PlayerScript player) => MyPlayer() == player;
    public static bool IsMyNetwTurn() => Settings.UseSimultaniousTurns ? true : CurrPlayer().IsOnMyNetwork();
    public static bool IsMyTurn(this PlayerScript player) => Settings.UseSimultaniousTurns ? true : CurrPlayer() == player;
    public static bool IsOnMyNetwork(this PlayerScript player) => PlayersOnMyNetwork().Any(x => x == player);
}