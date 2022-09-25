using System.Collections;
using System.Collections.Generic;

public class AbilityQueueSelectionDisplayScript : HexaEventCallback
{
    // PRIVE QUEUE VAN USER! (geen netwerk!)

    public AbilityQueueItemSelectionDisplayScript QueueAbilityDisplayPrefab;
    public ClearQueueDisplayScript ClearQueuePrefab;
    
    private List<AbilityQueueItemSelectionDisplayScript> AbilityDisplayGOs = new List<AbilityQueueItemSelectionDisplayScript>();
    private ClearQueueDisplayScript ClearQueueGo;

    protected override void OnPlayerAbilityQueue(PlayerScript player, Hex hex, AbilityType abilityType) => StartCoroutine(UpdateQueue(player));
    protected override void OnRemoveQueueItems(List<AbilityQueueItem> queueItems) => StartCoroutine(UpdateQueue(Netw.CurrPlayer()));
    protected override void OnNewPlayerTurn(PlayerScript player)
    {
        if(player.IsOnMyNetwork())
        {
            StartCoroutine(UpdateQueue(player, interactable: true));
        }        
    }

    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript player)
    {
        if (player.IsOnMyNetwork())
        {
            StartCoroutine(UpdateQueue(player, interactable: true));
        }
    }

    protected override void OnEndPlayerTurn(PlayerScript player, List<AbilityQueueItem> abilityQueue)
    {
        if (player.IsOnMyNetwork())
        {
            StartCoroutine(UpdateQueue(player, interactable: false));
        }           
    }

    private IEnumerator UpdateQueue(PlayerScript player, bool? interactable = null)
    {
        yield return Wait4Seconds.Get(0.1f);
        DestroyOldQueueItems();
        CreateNewQueueItems(player, interactable);

        if(AbilityDisplayGOs.Count > 0)
        {
            ClearQueueGo = Instantiate(ClearQueuePrefab, transform); 
        }
    }

    private void CreateNewQueueItems(PlayerScript player, bool? interactable = null)
    {
        var abilityQueueItems = player.GetComponent<PlayerAbilityQueueSelection>().AbilityQueueItems;
        foreach (var abilityQueueItem in abilityQueueItems)
        {
            var queueItemGo = Instantiate(QueueAbilityDisplayPrefab, transform);
            queueItemGo.SetAbility(abilityQueueItem);
            AbilityDisplayGOs.Add(queueItemGo);

            if(interactable.HasValue)
            {
                queueItemGo.Button.interactable = interactable.Value;
            }
        }
    }

    private void DestroyOldQueueItems()
    {
        for (int i = AbilityDisplayGOs.Count - 1; i >= 0 ; i--)
        {
            RemoveAbilityGO(AbilityDisplayGOs[i]);
        }
        Destroy(ClearQueueGo?.gameObject);
        ClearQueueGo = null;
    }

    private void RemoveAbilityGO(AbilityQueueItemSelectionDisplayScript item)
    {
        Destroy(item.gameObject);
        AbilityDisplayGOs.Remove(item);        
    }
}
