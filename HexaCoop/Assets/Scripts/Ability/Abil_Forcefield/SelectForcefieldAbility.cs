using UnityEngine;
using System.Collections;

public class SelectForcefieldAbility : MonoBehaviour, IAbilityAction
{
    public AbilityType AbilityType => AbilityType.Forcefield;

    public void InitAbilityAction()
    {
        DeselectAbility();
        //Textt.GameLocal("Select yourself to initialize Forcefield");
        StartCoroutine(SetTileSelectionInXSeconds(0.1f));
    }

    private IEnumerator SetTileSelectionInXSeconds(float seconds)
    {
        yield return Wait4Seconds.Get(seconds);
        var highlightOneTileSelection = MonoHelper.instance.GetHighlightOneTileSelection(gameObject);

        highlightOneTileSelection.SetOnlyConfirmTileSelection(Netw.CurrPlayer().CurrentHexTile);
        highlightOneTileSelection.CallbackOnTileSelectionConfirmed = OnTileSelectionConfirmed;
    }

    private void OnTileSelectionConfirmed(Hex hex)
    {
        NetworkAE.instance.PlayerAbility(Netw.CurrPlayer(), hex, AbilityType);
    }

    public void DeselectAbility()
    {
        Utils.Destroy(GetComponents<HighlightOneTileDisplayScript>());
    }

    public bool CanDoAbility(PlayerScript player) =>
        player?.GetComponent<PlayerAbilityHistory>().HasDoneAbilityThisTurn(AbilityType.Forcefield) == false &&
        player?.GetComponent<PlayerForcefieldScript>() == null;
}
