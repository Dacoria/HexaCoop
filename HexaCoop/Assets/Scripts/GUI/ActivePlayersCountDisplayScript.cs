using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using System;

public class ActivePlayersCountDisplayScript : HexaEventCallback
{
    public TMP_Text textNetwCountPlayers;
    public TMP_Text textCountAllPlayers;
    public TMP_Text textPlayersAlive;

    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript currPlayer)
    {
        Destroy(gameObject);
    }
    private void Update()
    {
        textNetwCountPlayers.text = "Active real players: " + NetworkHelper.instance.PlayerList.Count();
        textCountAllPlayers.text = "All active players: " + NetworkHelper.instance.GetAllPlayers().Count();
        textPlayersAlive.text = "Players Alive: " + NetworkHelper.instance.GetAllPlayers(isAlive: true).Count();

    }
}