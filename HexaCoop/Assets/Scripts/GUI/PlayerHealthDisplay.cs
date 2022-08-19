using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerHealthDisplay : HexaEventCallback
{
    [ComponentInject] private TMP_Text Text;

    private new void Awake()
    {
        base.Awake();
        Text.text = "";
    }

    private int counter;

    private void Update()
    {
        if(counter == 0)
        {
            UpdateHealthDisplay();
        }
        counter++;

        if(counter > 15)
        {
            counter = 0;
        }
    }

    private void UpdateHealthDisplay()
    {
        var currPlayer = Netw.CurrPlayer();

        if (currPlayer.IsOnMyNetwork())
        {
            // voor AI netwerk check
            Text.text = "Health: " + currPlayer.CurrentHP;
        }
        else
        {
            Text.text = "Health: " + Netw.MyPlayer().CurrentHP;
        }
    }
}
