using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class StartGameButtonScript : HexaEventCallback
{
    [ComponentInject] private Button button;
    [ComponentInject] private TMP_Text text;
    
    protected override void OnGridLoaded()
    {
        gridLoaded = true;
    }

    private bool hasStartedFirstGame;
    private bool hasAtLeastTwoPlayers;
    private bool gridLoaded;

    public void OnButtonClick()
    {
        GameHandler.instance.ResetGame();
        hasStartedFirstGame = true;
    }

    private void Update()
    {
        if (!hasAtLeastTwoPlayers)
        {
            hasAtLeastTwoPlayers = NetworkHelper.instance.GetAllPlayers().Count() >= 2;
        }

        text.text = hasStartedFirstGame ? "Reset" : "Start";
        button.interactable = gridLoaded && GameHandler.instance.GameStatus != GameStatus.GameEnded && hasAtLeastTwoPlayers;
    }
}
