using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AbilityPointsDisplayScript : HexaEventCallback
{
    [ComponentInject] private TMP_Text actionPointsText;
    public Image BarFilled;

    private new void Awake()
    {
        base.Awake();
        actionPointsText.text = "";
        BarFilled.fillAmount = 0;
    }

    private void Update()
    {
        var currPlayer = Netw.CurrPlayer();
        if (currPlayer.IsOnMyNetwork())
        {
            UpdateActionPoints(currPlayer);
        }
        else
        {
            //actionPointsText.text = "";
        }
    }
    
    private float targetBarFilledAmount;

    private void UpdateActionPoints(PlayerScript player)
    {
        var playerAction = player.GetComponent<PlayerActionPoints>();
        actionPointsText.text = player.CurrentAP + "/" + playerAction.ActionPointsLimit;
        targetBarFilledAmount = player.CurrentAP / (float)playerAction.ActionPointsLimit;

        BarFilled.fillAmount = Mathf.Lerp(BarFilled.fillAmount, targetBarFilledAmount, 3 * Time.deltaTime);
    }



}
