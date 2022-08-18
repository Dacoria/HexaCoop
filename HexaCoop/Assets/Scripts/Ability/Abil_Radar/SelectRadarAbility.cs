using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class SelectRadarAbility : MonoBehaviour, IAbilityAction
{
    public AbilityType AbilityType => AbilityType.Radar;

    public void InitAbilityAction()
    {
        DeselectAbility();
        //Textt.GameLocal("Select yourself to start radar");

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
        // doet nog niks met selected tile
        var otherPlayer = NetworkHelper.instance.OtherPlayerClosest(Netw.CurrPlayer());
        if (otherPlayer == null) { return; }

        var radarTarget = MonoHelper.instance.GetRandomTileAroundThisTile(otherPlayer.CurrentHexTile.HexCoordinates);

        NetworkActionEvents.instance.PlayerAbility(GameHandler.instance.CurrentPlayer(), radarTarget, AbilityType);
    } 

    public void DeselectAbility()
    {
        Utils.Destroy(GetComponents<HighlightOneTileDisplayScript>());
    }

    public bool CanDoAbility(PlayerScript player) => true;
}