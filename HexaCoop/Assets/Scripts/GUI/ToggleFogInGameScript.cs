using Chibi.Free;
using System.Collections.Generic;
using UnityEngine;

public class ToggleFogInGameScript : HexaEventCallback
{
    [ComponentInject] private CanvasGroup canvasGroup;
    [ComponentInject(Required.OPTIONAL)] private HideGoOnPunClient hideGoOnPunClient;

    private bool showEveryThing = false;

    private bool initialShowSurfacesInFog;
    private bool initialShowEnemiesInFog;
    private bool initialShowStructuresInFog;

    private FogGrid fogGrid;

    private new void Awake()
    {
        if(hideGoOnPunClient != null)
        {
            return;
        }

        base.Awake();

        initialShowSurfacesInFog = Settings.ShowSurfacesInFog;
        initialShowEnemiesInFog = Settings.ShowEnemiesInFog;
        initialShowStructuresInFog = Settings.ShowStructuresInFog;

        fogGrid = GameObject.FindObjectOfType<FogGrid>();
        //canvasGroup.alpha = 0;
    }

    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript player)
    {
        //canvasGroup.alpha = 1;
    }

    public void OnButtonClick()
    {
        showEveryThing = !showEveryThing;

        if(showEveryThing)
        {
            Settings.ShowSurfacesInFog = true;
            Settings.ShowEnemiesInFog = true;
            Settings.ShowStructuresInFog = true;

            Settings.ShowEverything = true;
        }
        else
        {
            Settings.ShowSurfacesInFog = initialShowSurfacesInFog;
            Settings.ShowEnemiesInFog = initialShowEnemiesInFog;
            Settings.ShowStructuresInFog = initialShowStructuresInFog;

            Settings.ShowEverything = false;
        }

        fogGrid.UpdateVisibility(Netw.MyPlayer());
    }
}
