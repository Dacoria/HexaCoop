using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NetworkHelper : MonoBehaviourPunCallbacks
{
    public static NetworkHelper instance;

    private List<PlayerScript> allPlayers;

    public List<PlayerScript> GetAllPlayers(bool? isAlive = null)
    {
        if(isAlive.HasValue)
        {
            return allPlayers.Where(x => x.IsAlive == isAlive.Value).ToList();
        }

        return allPlayers;
    }

    public Player[] PlayerList;

    private void Awake()
    {
        instance = this;
        allPlayers = new List<PlayerScript>();
        this.ComponentInject();
    }

    private void Start()
    {
        PlayerList = PhotonNetwork.PlayerList;
        RefreshPlayerGos();
    }

    public void RefreshPlayerGos()
    {
        this.allPlayers = GameObject.FindGameObjectsWithTag(Statics.TAG_PLAYER).Select(x => x.GetComponent<PlayerScript>()).ToList();
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        
        if(GameHandler.instance.GameStatus == GameStatus.ActiveRound)
        {
            if(GetAllPlayers(isAlive: true).Any(x => x.Id == otherPlayer.ActorNumber))
            {
                Textt.GameLocal("An active player has left the game! Reset the current game?");
            }
            else
            {
                Textt.GameLocal("A non-active player has left the game");
            }            
        }
        else
        {
            Textt.GameLocal("A player has left the game");
        }        

        PlayerList = PhotonNetwork.PlayerList;
        StartCoroutine(RefreshPlayerGosInXSeconds(0.1f)); // obj is niet direct weg; heel even wachten
    }


    private IEnumerator RefreshPlayerGosInXSeconds(float seconds)
    {
        yield return Wait4Seconds.Get(seconds);
        RefreshPlayerGos();
    }

    public PlayerScript ClosestPlayer(Vector3 positionToCompareDistance, PlayerScript playerToExclude = null)
    {
        return GetAllPlayers(isAlive: true).Where(x => playerToExclude == null ? true : x != playerToExclude)
            .OrderBy(x => Vector3.Distance(x.transform.position, positionToCompareDistance))
            .FirstOrDefault();
    }

    public PlayerScript OtherPlayerClosest(PlayerScript me)
    {
        return ClosestPlayer(me.transform.position, playerToExclude: me);
    }    

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);
        PlayerList = PhotonNetwork.PlayerList;
        RefreshPlayerGos();
    }      

    public List<PlayerScript> GetMyPlayers(bool includeAi, bool? isAlive = null)
    {
        var players = GetAllPlayers(isAlive: isAlive);
        var res = players
            .Where(player => player != null)
            .Where(player => includeAi || !player.IsAi)
            .Where(player => player.GetComponent<PhotonView>().OwnerActorNr == PhotonNetwork.LocalPlayer.ActorNumber)
            .ToList();

        return res;
    }

    public PlayerScript GetMyPlayer() => GetMyPlayers(includeAi: false).FirstOrDefault();    
}