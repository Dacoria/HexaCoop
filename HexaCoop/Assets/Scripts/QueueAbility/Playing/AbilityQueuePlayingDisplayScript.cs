using System.Collections;
using System.Collections.Generic;

public class AbilityQueuePlayingDisplayScript : HexaEventCallback
{
    public AbilityQueueItemPlayingDisplayScript QueueAbilityDisplayPrefab;
    private List<AbilityQueueItemPlayingDisplayScript> AbilityDisplayGOs = new List<AbilityQueueItemPlayingDisplayScript>();

    private void Start()
    {
        if (!Settings.UseQueueAbilities)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    protected override void OnStartAbilityQueue(List<AbilityQueueItem> abilityQueue)
    {
        DestroyOldQueueItems();

        foreach (var abilityQueueItem in abilityQueue)
        {
            var queueItemGo = Instantiate(QueueAbilityDisplayPrefab, transform);
            queueItemGo.SetAbility(abilityQueueItem.Id, abilityQueueItem.AbilityType, abilityQueueItem.Player.Color);
            AbilityDisplayGOs.Add(queueItemGo);            
        }
    }

    protected override void OnPlayerAbility(PlayerScript player, Hex hex, AbilityType abilityType, int queueId)
    {
        foreach (var queueItemDisplayGo in AbilityDisplayGOs)
        {
            queueItemDisplayGo.SetIsActiveAbility(queueItemDisplayGo.QueueItemId == queueId);
        }
    }

    protected override void OnPlayerAbilityNotExecuted(PlayerScript player, Hex hex, AbilityType abilityType, int queueId)
    {
        foreach (var queueItemDisplayGo in AbilityDisplayGOs)
        {
            queueItemDisplayGo.SetIsActiveAbility(queueItemDisplayGo.QueueItemId == queueId);
            if(queueItemDisplayGo.QueueItemId == queueId)
            {
                queueItemDisplayGo.SetIsNotExecuted();
            }            
        }
    }

    protected override void OnAllPlayersFinishedTurn()
    {
        foreach (var queueItemDisplayGo in AbilityDisplayGOs)
        {
            queueItemDisplayGo.SetIsActiveAbility(false);
        }
    }

    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript player)
    {
        if (player.IsOnMyNetwork())
        {
            DestroyOldQueueItems();
        }
    }

    private void DestroyOldQueueItems()
    {
        for (int i = AbilityDisplayGOs.Count - 1; i >= 0; i--)
        {
            RemoveAbilityGO(AbilityDisplayGOs[i]);
        }
    }

    private void RemoveAbilityGO(AbilityQueueItemPlayingDisplayScript item)
    {
        Destroy(item.gameObject);
        AbilityDisplayGOs.Remove(item);
    }
}
