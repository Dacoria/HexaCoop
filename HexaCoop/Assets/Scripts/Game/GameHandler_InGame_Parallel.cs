using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class GameHandler : HexaEventCallback
{
    private Dictionary<PlayerScript, List<AbilityQueueItem>> playersAbilityQueueDict = new Dictionary<PlayerScript, List<AbilityQueueItem>>();

    private void EndPlayerTurnWithQueue(PlayerScript player, List<AbilityQueueItem> abilityQueue)
    {
        if (!PhotonNetwork.IsMasterClient) { return; }

        playersAbilityQueueDict.Add(player, abilityQueue);

        if (playersAbilityQueueDict.Count == NetworkHelper.instance.GetAllPlayers(isAlive: true).Count())
        {
            // TODO -> CHANGE FASE?
            var totalAbilitieQueue = GetTotalAbilitieQueue(playersAbilityQueueDict);
            NetworkAE.instance.StartAbilityQueue(totalAbilitieQueue); // ook voor visueel maken van queue door andere spelers
            playersAbilityQueueDict.Clear();
        }
        else if (player.IsOnMyNetwork() && Netw.PlayersOnMyNetwork().Any(playerOnMyNetwork => !playersAbilityQueueDict.Keys.Contains(playerOnMyNetwork)))
        {
            // AI Speler + sim turns? dan AI speler pakken
            var playerOnMyNetworkWithoutTurn = Netw.PlayersOnMyNetwork().First(playerOnMyNetwork => !playersAbilityQueueDict.Keys.Contains(playerOnMyNetwork));
            NetworkAE.instance.NewPlayerTurn_Sequential(playerOnMyNetworkWithoutTurn);
        }
    }

    private List<AbilityQueueItem> GetTotalAbilitieQueue(Dictionary<PlayerScript, List<AbilityQueueItem>> playersAbilityQueueDict)
    {
        var result = new List<AbilityQueueItem>();
        for (int i = 0; i < 10; i++)
        {
            foreach (var player in PlayerQueueOrder)
            {
                if (playersAbilityQueueDict.TryGetValue(player, out var playerQueue))
                {
                    if (i <= playerQueue.Count() - 1)
                    {
                        result.Add(playerQueue[i]);
                    }
                }
            }
        }

        return result;
    }

    protected override void OnStartAbilityQueue(List<AbilityQueueItem> abilityQueueItems)
    {        
        if (!PhotonNetwork.IsMasterClient) { return; }
        
        var waitTime = 0.5f;
        foreach (var abilityQueueItem in abilityQueueItems)
        {
            StartCoroutine(InitAbilityInXSeconds(waitTime, abilityQueueItem));
            waitTime += abilityQueueItem.AbilityType.GetDuration() + 0.3f;
        }

        StartCoroutine(EndQueuePlayerTurnInXSeconds(waitTime));
    }

    private IEnumerator InitAbilityInXSeconds(float waitTime, AbilityQueueItem abilityQueueItem)
    {
        var hexCoorPlayerStartTurn = abilityQueueItem.Player.CurrentHexTile.HexCoordinates;
        if (!PhotonNetwork.IsMasterClient) { yield break; }
        yield return Wait4Seconds.Get(waitTime);

        if(!abilityQueueItem.Player.IsAlive)
        {
            // player kan dood zijn --> in dat geval, geen acties van hem meer
            NetworkAE.instance.PlayerAbilityNotExecuted(abilityQueueItem.Player, abilityQueueItem.Hex, abilityQueueItem.AbilityType, abilityQueueItem.Id);
            yield break;
        }


        var hexForAbil = DetermineHexForAbil(abilityQueueItem.Player, abilityQueueItem.AbilityType, abilityQueueItem.Hex, hexCoorPlayerStartTurn);

        if (hexForAbil != null && abilityQueueItem.Player.CanDoAbility(hexForAbil, abilityQueueItem.AbilityType))
        {
            NetworkAE.instance.Invoker_PlayerAbility(abilityQueueItem.Player, hexForAbil, abilityQueueItem.AbilityType, abilityQueueItem.Id);
        }
        else
        {
            var hexToSend = hexForAbil ?? abilityQueueItem.Hex;
            NetworkAE.instance.PlayerAbilityNotExecuted(abilityQueueItem.Player, hexToSend, abilityQueueItem.AbilityType, abilityQueueItem.Id);
        }        
    }

    private Hex DetermineHexForAbil(PlayerScript player, AbilityType abilityType, Hex hexSubmitted, UnityEngine.Vector3Int hexCoorPlayerStartTurn)
    {
        // voor movement --> directie ipv hex-resultaat --> hier bepalen (voor nu)

        if(player.CurrentHexTile.HexCoordinates == hexCoorPlayerStartTurn)
        {
            return hexSubmitted;
        }
        if(abilityType != AbilityType.Movement && abilityType != AbilityType.Jump && abilityType != AbilityType.Artillery)
        {
            return hexSubmitted;
        }

        var directions = hexCoorPlayerStartTurn.DeriveDirections(hexSubmitted.HexCoordinates);

        var newTargetHexCoor = player.CurrentHexTile.HexCoordinates.GetNewHexCoorFromDirections(directions);
        var hexResult = newTargetHexCoor.GetHex();

        return hexResult;
    }

    private IEnumerator EndQueuePlayerTurnInXSeconds(float waitTime)
    {
        if (!PhotonNetwork.IsMasterClient) { yield break; }
        yield return Wait4Seconds.Get(waitTime);

        NetworkAE.instance.AllPlayersFinishedTurn();
    }

    public List<PlayerScript> PlayerQueueOrder;

    private void SetNewPlayerOrderForSimultaniousTurns()
    {
        if(!PhotonNetwork.IsMasterClient) { return; }

        var allAliveplayers = NetworkHelper.instance.GetAllPlayers(isAlive: true);
        allAliveplayers.Shuffle();
        NetworkAE.instance.NewSimTurnsPlayOrder(allAliveplayers);
    }

    protected override void OnNewSimTurnsPlayOrder(List<PlayerScript> players)
    {
        PlayerQueueOrder = players;
    }
}
