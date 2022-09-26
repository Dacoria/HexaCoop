using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OrderOfPlayersDisplayScript : HexaEventCallback
{
    public GameObject OrderImage;
    public PlayerColorOrderDisplay PlayerColorOrderDisplayPrefab;
    public List<PlayerColorOrderDisplay> PlayerColorOrderDisplayGOs = new List<PlayerColorOrderDisplay>();
        
    protected override void OnNewSimTurnsPlayOrder(List<PlayerScript> players)
    {
        OrderImage.gameObject.SetActive(true);
        DestroyOldOrderItems();        

        foreach (PlayerScript player in players)
        {
            var playerColorGo = Instantiate(PlayerColorOrderDisplayPrefab, transform);
            playerColorGo.SetColor(player);
            PlayerColorOrderDisplayGOs.Add(playerColorGo);
        }
    }

    protected override void OnStartAbilityQueue(List<AbilityQueueItem> abilityQueue)
    {
        OrderImage.gameObject.SetActive(false);
        DestroyOldOrderItems();
    }

    protected override void OnEndPlayerTurn(PlayerScript player, List<AbilityQueueItem> abilityQueue)
    {
        var relatedOrderGo = PlayerColorOrderDisplayGOs.FirstOrDefault(x => x.player == player);
        if(relatedOrderGo != null)
        {
            relatedOrderGo.SetReady();
        }
    }

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
