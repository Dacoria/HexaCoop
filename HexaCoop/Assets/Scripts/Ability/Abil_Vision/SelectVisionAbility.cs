using System.Linq;
using UnityEngine;
public class SelectVisionAbility : MonoBehaviour, IAbilityAction
{
    public AbilityType AbilityType => AbilityType.Vision;

    public void InitAbilityAction()
    {
        DeselectAbility();
        //Textt.GameLocal("Select a tile for a Vision target");

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
            //Textt.GameLocal("Reselect tile to get vision on that tile");
        }
        reselectMessageHasBeenShown = true;
    }

    private void OnTileSelectionConfirmed(Hex hex)
    {
        NetworkAE.instance.PlayerAbility(GameHandler.instance.CurrentPlayer(), hex, AbilityType);
    }

    public void DeselectAbility()
    {
        Utils.Destroy(GetComponents<HighlightOneTileDisplayScript>());
    }
}