using UnityEngine;
using System.Linq;

public class ClearQueueDisplayScript : MonoBehaviour
{
    public void RemoveAllQueueItems()
    {
        var allQueueItemsOfPlayer = Netw.CurrPlayer().GetComponent<PlayerAbilityQueueSelection>().AbilityQueueItems;
        ActionEvents.RemoveQueueItems?.Invoke(allQueueItemsOfPlayer);
    }
}
