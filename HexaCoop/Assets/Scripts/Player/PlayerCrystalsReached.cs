using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Collections;

public class PlayerCrystalsReached : HexaEventCallback
{
    [ComponentInject] private PlayerScript playerScript;

    private int CrystalsReached;

    public void ReachedCrystal()
    {
        CrystalsReached++;

        if (CrystalsReached == 1)
        {
            Textt.GameLocal(playerScript.PlayerName + " has reached his/her first Crystal!");
        }
        else if (CrystalsReached >= 2)
        {
            Textt.GameLocal(playerScript.PlayerName + " has reached the 2 Crystals first!");
            GameHandler.instance.GameStatus = GameStatus.RoundEnded;

            // op MC
            StartCoroutine(RoundEndedInXSeconds(0.5f)); // zodat volgorde ook in chat klopt
        }
    }

    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript player)
    {
        CrystalsReached = 0;
    }

    private IEnumerator RoundEndedInXSeconds(float seconds)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            yield return new WaitForSeconds(seconds);
            NetworkActionEvents.instance.RoundEnded(true, playerScript);
        }        
    }
}