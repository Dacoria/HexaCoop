using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerExtraVisionRangeScript : HexaEventCallback
{
    [ComponentInject] private PlayerScript playerScript;

    public int AdditionalRange;

    private int TurnActivated;

    private new void Awake()
    {
        base.Awake();
        AdditionalRange = 1;
        TurnActivated = GameHandler.instance.CurrentTurn;
    }

    protected override void OnEndPlayerTurn(PlayerScript player)
    {
        if(playerScript == player)
        {
            // beurt van activatie + 1 andere beurt actief!
            if(GameHandler.instance.CurrentTurn >= TurnActivated + 1)
            {
                DestroyBinocular();
            }
        }
    }

    private void DestroyBinocular()
    {
        Destroy(this);
    }
}
