using System.Collections;
using UnityEngine;
using TMPro;

public class FireImmumeDisplayScript : HexaEventCallback
{
    [ComponentInject] private CanvasGroup canvasGroup;
    [ComponentInject] private TMP_Text Text;

    protected override void OnNewPlayerTurn(PlayerScript player) => StartCoroutine(UpdateFireImmumeDisplay());
    protected override void OnEndPlayerTurn(PlayerScript player) => StartCoroutine(UpdateFireImmumeDisplay());
    protected override void OnPlayerAbility(PlayerScript player, Hex hex, AbilityType abilityType) => StartCoroutine(UpdateFireImmumeDisplay());

    private void Start()
    {
        canvasGroup.alpha = 0;
    }

    private IEnumerator UpdateFireImmumeDisplay()
    {
        yield return Wait4Seconds.Get(0.1f); // wacht tot game is geupdatet

        var fireImmumeScript = Netw.CurrPlayer()?.GetComponent<PlayerFireImmumeScript>();

        if (Netw.IsMyNetwTurn() && fireImmumeScript != null)
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