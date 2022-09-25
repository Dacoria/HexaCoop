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

    private void Update()
    {
        if (Time.frameCount % 15 != 0) return;
        UpdateHealthDisplay();
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
