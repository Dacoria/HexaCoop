using UnityEngine;

public class SelectJumpAbility : MonoBehaviour, IAbilityAction
{
    public AbilityType AbilityType => AbilityType.Jump;

    private bool movementAbilIsActive;
    public void DeselectAbility()
    {
        NeighbourHexTileSelectionManager.instance.DeselectHighlightedNeighbours();
        movementAbilIsActive = false;
    }

    public void InitAbilityAction()
    {
        NeighbourHexTileSelectionManager.instance.HighlightMovementOptionsAroundPlayer(Netw.CurrPlayer(), excludeObstacles: !Settings.UseQueueAbilities, onlyMoveInOneDirection: true, range: 2);
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
            if (NeighbourHexTileSelectionManager.instance.SelectedPlayer == null)
            {
                // movement aanzetten gaat eerst via knoppen
                return;
            }            

            NeighbourHexTileSelectionManager.instance.HandleMouseClickForMove(Input.mousePosition, OnMovementTileSelected);
        }
    }

    private void OnMovementTileSelected(PlayerScript selectedPlayer, Hex hexSelected)
    {
        selectedPlayer.Ability(hexSelected, AbilityType);
    }

    public bool CanDoAbility(PlayerScript player) => player?.GetComponent<PlayerAbilityHistory>().HasDoneAbilityThisTurn(AbilityType.Meditate) == false;
}