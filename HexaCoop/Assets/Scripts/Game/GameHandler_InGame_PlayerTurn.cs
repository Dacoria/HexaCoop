using Photon.Pun;
using System.Collections.Generic;
using System.Linq;

public partial class GameHandler : HexaEventCallback
{    
    protected override void OnAllPlayersFinishedTurn()
    {
        CurrentTurn++; // Losse event call van maken? eigenlijk is de turn pas geeindigd na de enemy fase...

        if (!PhotonNetwork.IsMasterClient) { return; }

        // 1 doet de monster movements
        if (EnemyManager.instance.GetEnemies().Any())
        {
            DoEnemyFase(0.5f);
        }
        else
        {
            StartNewPlayerTurns();
        }
    }

    private void StartNewPlayerTurns()
    {
        if (!PhotonNetwork.IsMasterClient) { return; }
        NetworkAE.instance.NewPlayerTurn_Simultanious();
    }

    protected override void OnNewPlayerTurn(PlayerScript player)
    {
        SetNewPlayerOrder();

        if (player.IsOnMyNetwork())
        {
            // alleen op eigen netwerk setten (om zo ook AI te ondersteunen; vandaar zo)
            SetCurrentPlayer(player);
        }
    }
}
