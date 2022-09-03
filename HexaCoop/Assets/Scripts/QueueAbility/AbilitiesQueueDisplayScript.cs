using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AbilitiesQueueDisplayScript : HexaEventCallback
{
    // PRIVE QUEUE VAN USER! (geen netwerk!)

    public AbilityQueueDisplayScript QueueAbilityDisplayPrefab;

    private List<AbilityQueueDisplayScript> AbilityDisplayGOs = new List<AbilityQueueDisplayScript>();

    private void Start()
    {
        if (!Settings.UseQueueAbilities)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    protected override void OnPlayerAbilityQueue(PlayerScript player, Hex hex, AbilityType abilityType) => StartCoroutine(UpdateQueue());
    protected override void OnRemoveQueueItem(AbilityQueueItem queueItem) => StartCoroutine(UpdateQueue());
    protected override void OnNewPlayerTurn(PlayerScript player) => StartCoroutine(UpdateQueue());
    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript player) => StartCoroutine(UpdateQueue());

    //protected override void OnPlayerAbility(PlayerScript player, Hex hex, AbilityType abilityType)
    //{
    //    if(AbilitiesQueueScript.instance.AbilityQueueItems.Any(x => x.Player != player)) { return; }
    //
    //    if(AbilityDisplayGOs[0].IsActivated)
    //    {
    //        RemoveAbilityGO(AbilityDisplayGOs[0]);
    //    }
    //
    //    AbilityDisplayGOs[0].IsActivated = true;
    //}

    private IEnumerator UpdateQueue()
    {
        yield return Wait4Seconds.Get(0.1f);
        DestroyOldQueueItems();
        CreateNewQueueItems();
    }

    private void CreateNewQueueItems()
    {
        foreach (var abilityQueueItem in AbilitiesQueueScript.instance.AbilityQueueItems)
        {
            var queueItemGo = Instantiate(QueueAbilityDisplayPrefab, transform);
            queueItemGo.SetAbility(abilityQueueItem.Id, abilityQueueItem.AbilityType);
            AbilityDisplayGOs.Add(queueItemGo);
        }
    }

    private void DestroyOldQueueItems()
    {
        for (int i = AbilityDisplayGOs.Count - 1; i >= 0 ; i--)
        {
            RemoveAbilityGO(AbilityDisplayGOs[i]);
        }
    }

    private void RemoveAbilityGO(AbilityQueueDisplayScript item)
    {
        Destroy(item.gameObject);
        AbilityDisplayGOs.Remove(item);
    }
}
