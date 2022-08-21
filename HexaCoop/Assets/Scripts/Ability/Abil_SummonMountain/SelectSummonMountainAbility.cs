using System.Linq;
using UnityEngine;

public class SelectSummonMountainAbility: MonoBehaviour, IAbilityAction
{
    public AbilityType AbilityType => AbilityType.SummonMountain;

    private HighlightOneTileDisplayScript highlightOneTileSelection;
    public void InitAbilityAction()
    {
        Utils.Destroy(GetComponents<HighlightOneTileDisplayScript>());

        highlightOneTileSelection = MonoHelper.instance.GetHighlightOneTileSelection(gameObject);
        highlightOneTileSelection.CallbackOnTileSelection = OnTileSelection;
        highlightOneTileSelection.CallbackOnTileSelectionConfirmed = OnTileSelectionConfirmed;
    }


    private void OnTileSelection(Hex hex)
    {
        if(hex.HexStructureType != HexStructureType.None)
        {
            highlightOneTileSelection.Reset();
        }
    }
       
    private void OnTileSelectionConfirmed(Hex hex)
    {        
        NetworkAE.instance.PlayerAbility(Netw.CurrPlayer(), hex, AbilityType);
    }

    public void DeselectAbility()
    {
        Utils.Destroy(GetComponents<HighlightOneTileDisplayScript>());
    }

    public bool CanDoAbility(PlayerScript player) => true;
}
