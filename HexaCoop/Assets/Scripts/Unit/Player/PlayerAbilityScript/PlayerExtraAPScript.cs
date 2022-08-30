using System.Collections.Generic;

public class PlayerExtraAPScript : HexaEventCallback
{
    [ComponentInject] private PlayerScript playerScript;

    public int AdditionalAP;

    private int TurnActivated;

    private new void Awake()
    {
        base.Awake();
        TurnActivated = GameHandler.instance.CurrentTurn;
    }

    protected override void OnEndPlayerTurn(PlayerScript player, List<AbilityQueueItem> abilityQueue) => EndPlayerTurn(player);

    private void EndPlayerTurn(PlayerScript player)
    {
        if (playerScript == player)
        {
            // beurt van activatie + 1 andere beurt actief!
            if (GameHandler.instance.CurrentTurn >= TurnActivated + 1)
            {
                Destroy(this);
            }
        }
    }
}
