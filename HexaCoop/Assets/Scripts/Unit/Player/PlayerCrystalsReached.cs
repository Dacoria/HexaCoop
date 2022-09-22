using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.Collections;

public class PlayerCrystalsReached : HexaEventCallback
{
    [ComponentInject] private PlayerScript playerScript;
    private int cystalCountOnMap;

    private new void Awake()
    {
        base.Awake();
        cystalCountOnMap = GameObject.FindObjectsOfType<CrystalScript>().Count();
    }

    public List<Hex> CrystalHexesReached = new List<Hex>();

    public void ReachedCrystal(Hex hex)
    {
        if(CrystalHexesReached.Any(hexVisited => hexVisited == hex))
        {
            return;
        }
        CrystalHexesReached.Add(hex);

        if (CrystalHexesReached.Count() == 1)
        {
            Textt.GameLocal(playerScript.PlayerName + " has reached his/her first Crystal!");
        }
        else if (CrystalHexesReached.Count() == 2)
        {
            Textt.GameLocal(playerScript.PlayerName + " has reached his/her second Crystal!");
        }
        else if (CrystalHexesReached.Count() == 3)
        {
            Textt.GameLocal(playerScript.PlayerName + " has reached his/her third Crystal!");
        }
        else if (CrystalHexesReached.Count() >= cystalCountOnMap)
        {
            Textt.GameLocal(playerScript.PlayerName + " has reached all the Crystals first!");
            GameHandler.instance.GameStatus = GameStatus.RoundEnded;

            // op MC
            StartCoroutine(RoundEndedInXSeconds(0.5f)); // zodat volgorde ook in chat klopt
        }
    }

    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript player)
    {
        CrystalHexesReached = new List<Hex>();
    }

    private IEnumerator RoundEndedInXSeconds(float seconds)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            yield return Wait4Seconds.Get(seconds);
            NetworkAE.instance.RoundEnded(true, playerScript);
        }        
    }
}