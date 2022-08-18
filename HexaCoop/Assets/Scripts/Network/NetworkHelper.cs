using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NetworkHelper : MonoBehaviourPunCallbacks
{
    public static NetworkHelper instance;

    private List<PlayerScript> allPlayers;

    public List<PlayerScript> GetAllPlayers(bool? areAlive = null)
    {
        if(areAlive.HasValue)
        {
            if(areAlive.Value)
            {
                return allPlayers.Where(x => x.PlayerIsAlive).ToList();
            }
            else
            {
                return allPlayers.Where(x => !x.PlayerIsAlive).ToList();
            }            
        }

        return allPlayers;
    }

    public Photon.Realtime.Player[] PlayerList;

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
        // voor nu: alleen toevoegen (want door tags pak je niet inactieve obj)
        var playersEnabledWithTag = GameObject.FindGameObjectsWithTag(Statics.TAG_PLAYER).Select(x => x.GetComponent<PlayerScript>()).ToList();

        foreach(var player in playersEnabledWithTag)
        {
            if(!allPlayers.Any(x => x.PlayerId == player.PlayerId))
            {
                allPlayers.Add(player);
            }
        }
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        base.OnPlayerLeftRoom(otherPlayer);
        NetworkActionEvents.instance.EndGame();
        Textt.GameLocal("A player has left the game! This is not supported. Reconnect for a new game");

        PlayerList = PhotonNetwork.PlayerList;
        RefreshPlayerGos();
    }

    public PlayerScript ClosestPlayer(Vector3 positionToCompareDistance, PlayerScript playerToExclude = null)
    {
        return GetAllPlayers(areAlive: true).Where(x => playerToExclude == null ? true : x != playerToExclude)
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

    public List<PlayerScript> GetMyAlivePlayers(bool includeAi, bool? areAlive = null)
    {
        var players = GetAllPlayers(areAlive: areAlive);
        var res = players
            .Where(player => player != null)
            .Where(player => includeAi || !player.IsAi)
            .Where(player => player.GetComponent<PhotonView>().OwnerActorNr == PhotonNetwork.LocalPlayer.ActorNumber)
            .ToList();

        return res;
    }

    public PlayerScript GetMyPlayer()
    {
        return GetMyAlivePlayers(false).FirstOrDefault();
    }

    public void SetGameText(string gameText, PlayerScript playerFilter)
    {      
        if (playerFilter == null || playerFilter.IsOnMyNetwork())
        {
            GameDialogScript.instance.AddText(gameText);
        }        
    } 
}