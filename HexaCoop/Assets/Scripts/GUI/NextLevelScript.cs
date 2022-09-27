using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelScript : HexaEventCallback
{
    [ComponentInject] private CanvasGroup canvasGroup;
    public bool SameLevel;
    void Start()
    {
        canvasGroup.alpha = 0;
    }

    protected override void OnEndRound(bool reachedMiddle, PlayerScript pWinner)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            canvasGroup.alpha = 1;
        }
    }
    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript player)
    {
        canvasGroup.alpha = 0;
    }

    public void ClickButton()
    {
        if(SameLevel)
        {
            SceneHandler.instance.LoadSameScene();
        }
        else
        {
            SceneHandler.instance.LoadNextScene();
        }        
    }
}
