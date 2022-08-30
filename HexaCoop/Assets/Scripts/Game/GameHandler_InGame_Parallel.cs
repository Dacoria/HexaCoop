using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public partial class GameHandler : HexaEventCallback
{
    private Dictionary<PlayerScript, List<AbilityQueueItem>> playersAbilityQueueDict = new Dictionary<PlayerScript, List<AbilityQueueItem>>();


    private void EndPlayerTurnWithQueue(PlayerScript player, List<AbilityQueueItem> abilityQueue)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            playersAbilityQueueDict.Add(player, abilityQueue);
            if (playersAbilityQueueDict.Count == NetworkHelper.instance.GetAllPlayers(isAlive: true).Count())
            {
                // TODO -> CHANGE FASE?
                StartProcessingQueueAbilities();
            }
        }
    }

    private void StartProcessingQueueAbilities()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            var waitTime = 0.5f;

            for (int i = 0; i < 10; i ++)
            {
                foreach (var playersAbilityQueue in playersAbilityQueueDict)
                {
                    if(i <= playersAbilityQueue.Value.Count() - 1)
                    {
                        StartCoroutine(InitAbilityInXSeconds(waitTime, playersAbilityQueue.Value[i]));
                        waitTime += 2f;
                    }
                }
            }

            StartCoroutine(EndQueuePlayerTurnInXSeconds(waitTime));
        }
    }

    private IEnumerator InitAbilityInXSeconds(float waitTime, AbilityQueueItem abilityQueueItem)
    {
        yield return Wait4Seconds.Get(waitTime);
        NetworkAE.instance.Invoker_PlayerAbility(abilityQueueItem.Player, abilityQueueItem.Hex, abilityQueueItem.AbilityType);
    }

    private IEnumerator EndQueuePlayerTurnInXSeconds(float waitTime)
    {
        yield return Wait4Seconds.Get(waitTime);
        playersAbilityQueueDict.Clear();

        NetworkAE.instance.AllPlayersFinishedTurn();
    }
}
