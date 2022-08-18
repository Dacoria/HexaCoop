using UnityEngine;

public class SelectMovementAbility : MonoBehaviour, IAbilityAction
{
    public AbilityType AbilityType => AbilityType.Movement;

    private bool movementAbilIsActive;
    public void DeselectAbility()
    {
        HexTileSelectionManager.instance.DeselectHighlightedNeighbours();
        movementAbilIsActive = false;
    }

    public void InitAbilityAction()
    {
        HexTileSelectionManager.instance.HighlightMovementOptionsAroundPlayer(GameHandler.instance.CurrentPlayer());
        //Textt.GameLocal("Select the tile to move to");
        movementAbilIsActive = true;
    }

    private void Update()
    {
        if(!movementAbilIsActive)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (HexTileSelectionManager.instance.SelectedPlayer == null)
            {
                // movement aanzetten gaat eerst via knoppen
                return;
            }            

            HexTileSelectionManager.instance.HandleMouseClickForMove(Input.mousePosition, OnMovementTileSelected);
        }
    }

    private void OnMovementTileSelected(PlayerScript selectedPlayer, Hex hexSelected)
    {
        NetworkActionEvents.instance.PlayerAbility(selectedPlayer, hexSelected, AbilityType);
    }

    public bool CanDoAbility(PlayerScript player) => !player.GetComponent<PlayerAbilityHistory>().HasDoneAbilityThisTurn(AbilityType.Meditate);
}