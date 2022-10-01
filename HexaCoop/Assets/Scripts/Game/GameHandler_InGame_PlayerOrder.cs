using Photon.Pun;
using System.Collections.Generic;

public partial class GameHandler : HexaEventCallback
{
    private int turnPlayerOrderUpdated;
    public List<PlayerScript> PlayerQueueOrder;

    private void SetNewPlayerOrder()
    {
        if(!PhotonNetwork.IsMasterClient) { return; }
        if(CurrentTurn == turnPlayerOrderUpdated) { return; }

        turnPlayerOrderUpdated = CurrentTurn; // reden: Local host + AI = meerdere calls.
        var allAliveplayers = NetworkHelper.instance.GetAllPlayers(isAlive: true);
        allAliveplayers.Shuffle();
        NetworkAE.instance.NewSimTurnsPlayOrder(allAliveplayers);
    }

    protected override void OnNewSimTurnsPlayOrder(List<PlayerScript> players)
    {
        PlayerQueueOrder = players;
    }
}
