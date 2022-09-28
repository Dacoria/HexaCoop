using System.Linq;
using UnityEngine;
public class SelectSwapTilesAbility : MonoBehaviour, IAbilityAction
{
    public AbilityType AbilityType => AbilityType.SwapTiles;

    public void InitAbilityAction()
    {
        Utils.Destroy(GetComponents<HighlightOneTileDisplayScript>());

        var highlightOneTileSelection = MonoHelper.instance.GetHighlightOneTileSelection(gameObject);
        highlightOneTileSelection.CallbackOnTileSelection = OnTileSelection;
        highlightOneTileSelection.CallbackOnTileSelectionConfirmed = OnTileSelectionConfirmed;

        reselectMessageHasBeenShown = false;
    }

    private bool reselectMessageHasBeenShown;

    private void OnTileSelection(Hex hex)
    {
        if (!reselectMessageHasBeenShown)
        {
            //Textt.GameLocal("Reselect tile to fire the rocket!");
        }
        reselectMessageHasBeenShown = true;
    }

    private void OnTileSelectionConfirmed(Hex hex)
    {
        Netw.CurrPlayer().Ability(hex, AbilityType);
    }

    public void DeselectAbility()
    {
        Utils.Destroy(GetComponents<HighlightOneTileDisplayScript>());
    }

    public bool CanDoAbility(PlayerScript player) => true;

}