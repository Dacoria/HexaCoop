using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class GameHandler : HexaEventCallback
{
    private Dictionary<PlayerScript, List<AbilityQueueItem>> playersAbilityQueueDict = new Dictionary<PlayerScript, List<AbilityQueueItem>>();

    protected override void OnEndPlayerTurn(PlayerScript player, List<AbilityQueueItem> abilityQueue)
    {        
        if (!PhotonNetwork.IsMasterClient) { return; }
        if(playersAbilityQueueDict.ContainsKey(player)) { return; }

        playersAbilityQueueDict.Add(player, abilityQueue);

        if (playersAbilityQueueDict.Count == NetworkHelper.instance.GetAllPlayers(isAlive: true).Count())
        {
            // TODO -> CHANGE FASE?
            var totalAbilitieQueue = DetermineTotalAbilitieQueue(playersAbilityQueueDict);
            NetworkAE.instance.StartAbilityQueue(totalAbilitieQueue); // ook voor visueel maken van queue door andere spelers
            playersAbilityQueueDict.Clear();
        }
        else if (player.IsOnMyNetwork() && Netw.PlayersOnMyNetwork(isAlive: true).Any(playerOnMyNetwork => !playersAbilityQueueDict.Keys.Contains(playerOnMyNetwork)))
        {
            // AI Speler + sim turns? dan AI speler pakken
            var playerOnMyNetworkWithoutTurn = Netw.PlayersOnMyNetwork(isAlive: true).First(playerOnMyNetwork => !playersAbilityQueueDict.Keys.Contains(playerOnMyNetwork));
            NetworkAE.instance.NewPlayerTurn_Sequential(playerOnMyNetworkWithoutTurn);
        }
    }

    public List<AbilityQueueItem> DetermineTotalAbilitieQueue(Dictionary<PlayerScript, List<AbilityQueueItem>> playersAbilityQueueDict)
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
}
