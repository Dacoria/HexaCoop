using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteAbilityHeightCorrection : HexaEventCallback
{
    [ComponentInject] private Button Button;    
    [ComponentInject] private Image ImageAbility;

    public Transform StartPosition;
    public Transform LoweredPosition;

    private Image ImageButton;

    protected override void OnNewRoundStarted(List<PlayerScript> players, PlayerScript player)
    {
        StartCoroutine(Init(0.1f));
    }
    private IEnumerator Init(float waitTime)
    {
        yield return Wait4Seconds.Get(waitTime);

        this.ComponentInject();
        ImageButton = Button.GetComponent<Image>();
    }

    private void Update()
    {
        if(ImageButton == null)
        {
            ImageButton = Button.GetComponent<Image>();
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
