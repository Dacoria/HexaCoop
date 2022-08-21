using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowOnStartGame : HexaEventCallback
{
    public GameObject targetGo;

    void Start()
    {
        targetGo.SetActive(false);
    }

    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript player)
    {
        targetGo.SetActive(true);
        Destroy(this);
    }
}
