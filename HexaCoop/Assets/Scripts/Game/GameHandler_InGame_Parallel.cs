using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

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
        else if (Netw.PlayersOnMyNetwork().Any(playerOnMyNetwork => !playersAbilityQueueDict.Keys.Contains(playerOnMyNetwork)))
        {
            var playerOnMyNetworkWithoutTurn = Netw.PlayersOnMyNetwork().First(playerOnMyNetwork => !playersAbilityQueueDict.Keys.Contains(playerOnMyNetwork));
            NetworkAE.instance.NewPlayerTurn(playerOnMyNetworkWithoutTurn);            
        }
    }

    private List<AbilityQueueItem> GetTotalAbilitieQueue(Dictionary<PlayerScript, List<AbilityQueueItem>> playersAbilityQueueDict)
    {
        var result = new List<AbilityQueueItem>();
        for (int i = 0; i < 10; i++)
        {
            foreach (var playersAbilityQueue in playersAbilityQueueDict)
            {
                if (i <= playersAbilityQueue.Value.Count() - 1)
                {
                    result.Add(playersAbilityQueue.Value[i]);
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
            waitTime += 2f;
        }

        StartCoroutine(EndQueuePlayerTurnInXSeconds(waitTime));
        
    }

    private IEnumerator InitAbilityInXSeconds(float waitTime, AbilityQueueItem abilityQueueItem)
    {
        if (!PhotonNetwork.IsMasterClient) { yield break; }
        yield return Wait4Seconds.Get(waitTime);
        NetworkAE.instance.Invoker_PlayerAbility(abilityQueueItem.Player, abilityQueueItem.Hex, abilityQueueItem.AbilityType);
    }

    private IEnumerator EndQueuePlayerTurnInXSeconds(float waitTime)
    {
        if (!PhotonNetwork.IsMasterClient) { yield break; }
        yield return Wait4Seconds.Get(waitTime);
        playersAbilityQueueDict.Clear();

        NetworkAE.instance.AllPlayersFinishedTurn();
    }
}
