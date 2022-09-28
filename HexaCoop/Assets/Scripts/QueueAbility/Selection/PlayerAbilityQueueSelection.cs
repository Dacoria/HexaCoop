using System.Linq;
using System.Collections.Generic;

public class PlayerAbilityQueueSelection : HexaEventCallback
{
    public List<AbilityQueueItem> AbilityQueueItems = new List<AbilityQueueItem>();
    public AbilityQueueItem Get(int queueId) => AbilityQueueItems.Single(x => x.Id == queueId);

    protected override void OnPlayerAbilityQueue(PlayerScript player, Hex hex, Hex hex2, AbilityType abilityType) => AbilityQueueItems.Add(new AbilityQueueItem(player, hex, hex2, abilityType));
    protected override void OnRemoveQueueItems(List<AbilityQueueItem> queueItems)
    {
        for (int i = queueItems.Count - 1; i >= 0 ; i--)
        {
            var queueItem = queueItems[i];
            AbilityQueueItems.Remove(AbilityQueueItems.FirstOrDefault(x => x.Id == queueItem.Id));
        }
    }

    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript player) => AbilityQueueItems.RemoveAll(x => true);
    protected override void OnNewPlayerTurn(PlayerScript player) => AbilityQueueItems.RemoveAll(x => true);
}
