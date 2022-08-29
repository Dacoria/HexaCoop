using System.Linq;
using System.Collections.Generic;

public class AbilitiesQueueScript : HexaEventCallback
{
    public static AbilitiesQueueScript instance;
    public List<AbilityQueueItem> AbilityQueueItems = new List<AbilityQueueItem>();

    private new void Awake()
    {
        instance = this;
        base.Awake();

        if(!Settings.UseQueueAbilities)
        {
            Destroy(this);
        }
    }

    public AbilityQueueItem Get(int queueId) => AbilityQueueItems.Single(x => x.Id == queueId);

    protected override void OnPlayerAbilityQueue(PlayerScript player, Hex hex, AbilityType abilityType) => AbilityQueueItems.Add(new AbilityQueueItem(player, hex, abilityType));
    protected override void OnRemoveQueueItem(AbilityQueueItem queueItem) => AbilityQueueItems.Remove(AbilityQueueItems.Single(x => x.Id == queueItem.Id));
    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript player) => AbilityQueueItems.RemoveAll(x => true);
    protected override void OnNewPlayerTurn(PlayerScript player) => AbilityQueueItems.RemoveAll(x => true);
}
