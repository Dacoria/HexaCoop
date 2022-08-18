using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Linq;
public static class Netw
{
    public static PlayerScript MyPlayer() => NetworkHelper.instance.GetMyPlayer();
    public static PlayerScript CurrPlayer() => GameHandler.instance.CurrentPlayer();
    public static List<PlayerScript> PlayersOnMyNetwork() => NetworkHelper.instance.GetMyAlivePlayers(includeAi: true);

    public static bool IsMe(this PlayerScript player) => MyPlayer() == player;
    public static bool IsMyNetwTurn() => CurrPlayer().IsOnMyNetwork();
    public static bool IsMyTurn(this PlayerScript player) => CurrPlayer() == player;
    public static bool IsOnMyNetwork(this PlayerScript player) => PlayersOnMyNetwork().Any(x => x == player);

    
}