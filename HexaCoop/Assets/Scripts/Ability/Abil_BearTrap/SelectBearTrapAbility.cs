using System.Linq;
using UnityEngine;

public class SelectBearTrapAbility : MonoBehaviour, IAbilityAction
{
    public AbilityType AbilityType => AbilityType.BearTrap;

    public void InitAbilityAction()
    {
        //Textt.GameLocal("Select a tile to place a trap");
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
            //Textt.GameLocal("Reselect tile to confirm trap placement move");
        }
        reselectMessageHasBeenShown = true;
    }

    private void OnTileSelectionConfirmed(Hex hex)
    {
        // doet nog niks met selected tile
        if(hex.HexStructureType.In(HexStructureType.Well, HexStructureType.Mountain, HexStructureType.Market, HexStructureType.Crystal))
        {
            // reset - mag niet
            InitAbilityAction();
            Textt.GameLocal("Beartrap on Crystal/Well/Mountain is not allowed");
        }
        else
        {
            NetworkAE.instance.PlayerAbility(GameHandler.instance.CurrentPlayer(), hex, AbilityType);
        }
        
    } 

    public void DeselectAbility()
    {
        Utils.Destroy(GetComponents<HighlightOneTileDisplayScript>());

    }
    public bool CanDoAbility(PlayerScript player) => player?.GetComponent<PlayerAbilityHistory>().AbilityDoneThisTurnCount(AbilityType.BearTrap) == 0;
}