using System.Collections.Generic;

public class DestroyOnGameStart : HexaEventCallback
{
    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript player)
    {
        Destroy(gameObject);
    }
}
