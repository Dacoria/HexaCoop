using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class EndTurnButtonScript : HexaEventCallback
{
    // Wordt vanuit Button updater & Button event (canvas) geupdatet!

    [ComponentInject] public Button Button;

    public void OnEndTurnButtonClick()
    {
        if (GameHandler.instance.CurrentPlayer().IsOnMyNetwork())
        {
            NetworkActionEvents.instance.EndPlayerTurn(GameHandler.instance.CurrentPlayer());
        }
    }
}
