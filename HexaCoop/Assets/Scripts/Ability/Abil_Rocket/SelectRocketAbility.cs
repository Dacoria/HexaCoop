using System.Linq;
using UnityEngine;

public class SelectRocketAbility : MonoBehaviour, IAbilityAction
{
    public AbilityType AbilityType => AbilityType.Rocket;

    public void InitAbilityAction()
    {
        Utils.Destroy(GetComponents<HighlightOneTileDisplayScript>());
        //Textt.GameLocal("Select a tile to fire your rocket");

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
        NetworkAE.instance.PlayerAbility(Netw.CurrPlayer(), hex, AbilityType);
    }

    public void DeselectAbility()
    {
        Utils.Destroy(GetComponents<HighlightOneTileDisplayScript>());
    }

    public bool CanDoAbility(PlayerScript player) => player?.GetComponent<PlayerAbilityHistory>().HasDoneAbilityThisTurn(AbilityType.Rocket) == false;
}
