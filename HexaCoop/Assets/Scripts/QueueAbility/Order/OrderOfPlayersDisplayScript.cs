using System.Collections.Generic;

public class OrderOfPlayersDisplayScript : HexaEventCallback
{
    public PlayerColorOrderDisplay PlayerColorOrderDisplayPrefab;
    public List<PlayerColorOrderDisplay> PlayerColorOrderDisplayGOs = new List<PlayerColorOrderDisplay>();

    private void Start()
    {
        if (!Settings.UseQueueAbilities)
        {
            Destroy(transform.parent.gameObject);
        }
    }

    protected override void OnNewSimTurnsPlayOrder(List<PlayerScript> players)
    {       
        DestroyOldOrderItems();

        foreach (PlayerScript player in players)
        {
            var playerColorGo = Instantiate(PlayerColorOrderDisplayPrefab, transform);
            playerColorGo.SetColor(player.Color);
            PlayerColorOrderDisplayGOs.Add(playerColorGo);
        }
    }

    protected override void OnStartAbilityQueue(List<AbilityQueueItem> abilityQueue) => DestroyOldOrderItems();

    private void DestroyOldOrderItems()
    {
        for (int i = PlayerColorOrderDisplayGOs.Count - 1; i >= 0; i--)
        {
            RemovePlayerOrderGO(PlayerColorOrderDisplayGOs[i]);
        }
    }

    private void RemovePlayerOrderGO(PlayerColorOrderDisplay item)
    {
        Destroy(item.gameObject);
        PlayerColorOrderDisplayGOs.Remove(item);
    }
}
