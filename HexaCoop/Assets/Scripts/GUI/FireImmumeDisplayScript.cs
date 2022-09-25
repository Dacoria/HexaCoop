using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class FireImmumeDisplayScript : HexaEventCallback
{
    [ComponentInject] private CanvasGroup canvasGroup;
    [ComponentInject] private TMP_Text Text;

    protected override void OnNewPlayerTurn(PlayerScript player) => StartCoroutine(UpdateFireImmumeDisplay());
    protected override void OnEndPlayerTurn(PlayerScript player, List<AbilityQueueItem> abilityQueue) => StartCoroutine(UpdateFireImmumeDisplay());
    protected override void OnPlayerAbility(PlayerScript player, Hex hex, AbilityType abilityType, int queueId) => StartCoroutine(UpdateFireImmumeDisplay());

    private void Start()
    {
        canvasGroup.alpha = 0;
    }

    private IEnumerator UpdateFireImmumeDisplay()
    {
        yield return Wait4Seconds.Get(0.1f); // wacht tot game is geupdatet

        var fireImmumeScript = Netw.CurrPlayer()?.GetComponent<PlayerFireImmumeScript>();

        if (fireImmumeScript != null)
        {
            canvasGroup.alpha = 1;
            Text.text = "Turn(s): " + fireImmumeScript.TurnsLeft.ToString();
        }
        else
        {
            canvasGroup.alpha = 0;
        }
    }

}