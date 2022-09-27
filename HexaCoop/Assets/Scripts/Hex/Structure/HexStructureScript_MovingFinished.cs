using Photon.Pun;
using System.Collections;
using UnityEngine;

public partial class HexStructureScript: HexaEventCallback
{
    protected override void OnPlayerMovingFinished(PlayerScript player, Hex hexMovedTo)
    {
        if(hexMovedTo == hex)
        {
            switch(hex.HexStructureType)
            {
                case HexStructureType.Portal:
                    hex.GetComponentInChildren<PortalScript>().PlayerSteppedOnStructure(player);
                    break;
                case HexStructureType.Forest:
                    Textt.GameLocal("Forest: Lose 2 Actionpoints", playerFilter: player);
                    player.GetComponent<PlayerActionPoints>().DecreaseAP(2);
                    break;
                case HexStructureType.Hill:
                    Textt.GameLocal("Hill: Lose 1 Actionpoint", playerFilter: player);
                    player.GetComponent<PlayerActionPoints>().DecreaseAP(1);
                    break;
                case HexStructureType.Crystal:

                    // text update gaat via crystals reached script
                    //var crystalReachedScript = player.gameObject.GetAdd<PlayerCrystalsReached>();
                    //crystalReachedScript.ReachedCrystal(hex);

                    Textt.GameLocal(player.PlayerName + " has reached the Crystals first!");
                    GameHandler.instance.GameStatus = GameStatus.RoundEnded;

                    if(PhotonNetwork.IsMasterClient)
                    {
                        StartCoroutine(RoundEndedInXSeconds(0.5f, player)); // zodat volgorde ook in chat klopt
                    }
                    break;

            }
        }
    }

    private IEnumerator RoundEndedInXSeconds(float seconds, PlayerScript player)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            yield return Wait4Seconds.Get(seconds);
            NetworkAE.instance.RoundEnded(true, player);
        }
    }
}
