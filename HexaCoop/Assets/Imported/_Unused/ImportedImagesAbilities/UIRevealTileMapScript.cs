using Chibi.Free;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIRevealTileMapScript : HexaEventCallback
{
    [ComponentInject] private Button button;

    private new IEnumerator Start()
    {
        base.Start();
        yield return new UnityEngine.WaitForSeconds(0.3f); 

        if (Settings.ShowOnlyPlainHexesBeforeGameStart)
        {
            MonoHelper.instance.SetHexLayoutBeforeGame(originalLayout: false); 
        }
        else 
        {
            Destroy(gameObject);
        }
    }

    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript player)
    {
        Destroy(gameObject);
    }

    public void OnButtonClick()
    {
        button.interactable = false;
        MonoHelper.instance.SetHexLayoutBeforeGame(true);
        Destroy(gameObject);
    }    
}
