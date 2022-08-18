using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectMeditateAbility : MonoBehaviour, IAbilityAction
{
    public AbilityType AbilityType => AbilityType.Meditate;

    public void InitAbilityAction()
    {
        DeselectAbility();
        //Textt.GameLocal("Select yourself to start meditating");

        StartCoroutine(SetTileSelectionInXSeconds(0.1f));
    }

    private IEnumerator SetTileSelectionInXSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        var highlightOneTileSelection = MonoHelper.instance.GetHighlightOneTileSelection(gameObject);

        highlightOneTileSelection.SetOnlyConfirmTileSelection(Netw.CurrPlayer().CurrentHexTile);
        highlightOneTileSelection.CallbackOnTileSelectionConfirmed = OnTileSelectionConfirmed;
    }

       
    private void OnTileSelectionConfirmed(Hex hex)
    {
        var radarTarget = MonoHelper.instance.GetRandomTileAroundThisTile(hex.HexCoordinates);
        NetworkActionEvents.instance.PlayerAbility(Netw.CurrPlayer(), radarTarget, AbilityType);
    }

    public void DeselectAbility()
    {
        Utils.Destroy(GetComponents<HighlightOneTileDisplayScript>());
    }

    public bool CanDoAbility(PlayerScript player) => player.GetComponent<PlayerAbilityHistory>().HasDoneAbilityThisTurn(AbilityType.Meditate);
}
