using System.Linq;
using UnityEngine;
public class SelectWaitAbility : MonoBehaviour, IAbilityAction
{
    public AbilityType AbilityType => AbilityType.Wait;

    private bool abilIsActive;
    public void DeselectAbility()
    {
        NeighbourHexTileSelectionManager.instance.DeselectHighlightedNeighbours();
        abilIsActive = false;
    }

    public void InitAbilityAction()
    {
        if (Settings.UseQueueAbilities)
        {
            Netw.CurrPlayer().Ability(Netw.CurrPlayer().CurrentHexTile, AbilityType);
        }
        else
        {
            NeighbourHexTileSelectionManager.instance.HighlightNeighbourOptionsAroundPlayer(Netw.CurrPlayer());
            abilIsActive = true;
        }
    }

    private void Update()
    {
        if (!abilIsActive)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (NeighbourHexTileSelectionManager.instance.SelectedPlayer == null)
            {
                return;
            }

            NeighbourHexTileSelectionManager.instance.HandleMouseClickForMove(Input.mousePosition, OnMovementTileSelected);
        }
    }

    private void OnMovementTileSelected(PlayerScript selectedPlayer, Hex hexSelected)
    {
        selectedPlayer.Ability(hexSelected, AbilityType);
    }
}