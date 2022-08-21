using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class SpriteAbilityHeightCorrection : HexaEventCallback
{
    [ComponentInject] private Button Button;    
    [ComponentInject] private Image ImageAbility;

    public Transform StartPosition;
    public Transform LoweredPosition;

    private Image ImageButton;

    protected override void OnNewRoundStarted(List<PlayerScript> players, PlayerScript player)
    {
        Init();
    }
    private void Init()
    {
        this.ComponentInject();
        this.ImageButton = Button.GetComponent<Image>();
    }

    private void Update()
    {
        if(ImageButton == null)
        {
            return;
        }

        if (ImageButton.sprite.name.Contains("pressed"))
        {
            ImageAbility.transform.position = LoweredPosition.position;
        }
        else
        {
            ImageAbility.transform.position = StartPosition.position;
        }
    }
}
