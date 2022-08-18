using System.Collections;
using UnityEngine;
public class SelectBinocularAbility : MonoBehaviour, IAbilityAction
{
    public AbilityType AbilityType => AbilityType.Binocular;

    public void InitAbilityAction()
    {
        DeselectAbility();
        //Textt.GameLocal("Select yourself to start Binocular");

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
        NetworkActionEvents.instance.PlayerAbility(Netw.CurrPlayer(), hex, AbilityType);
    }

    public void DeselectAbility()
    {
        Utils.Destroy(GetComponents<HighlightOneTileDisplayScript>());
    }

    public bool CanDoAbility(PlayerScript player) =>
        !player.GetComponent<PlayerAbilityHistory>().HasDoneAbilityThisTurn(AbilityType.Binocular) &&
        !player.GetComponent<PlayerAbilityHistory>().HasDoneAbilityPreviousTurn(AbilityType.Binocular);
}