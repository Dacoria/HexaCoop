using System.Collections;
using System.Linq;
using UnityEngine;

public class SelectBombExplosionAbility : MonoBehaviour, IAbilityAction
{
    public AbilityType AbilityType => AbilityType.BombExplosion;

    private bool abilIsActive;
    public void DeselectAbility()
    {
        NeighbourHexTileSelectionManager.instance.DeselectHighlightedNeighbours();
        abilIsActive = false;
    }

    public void InitAbilityAction()
    {
        NeighbourHexTileSelectionManager.instance.HighlightMovementOptionsAroundPlayer(Netw.CurrPlayer(), excludeObstacles: !Settings.UseQueueAbilities, onlyMoveInOneDirection: true, range: 2, showOnlyFurthestRange: false);
        abilIsActive = true;
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
                // movement aanzetten gaat eerst via knoppen
                return;
            }

            NeighbourHexTileSelectionManager.instance.HandleMouseClickForMove(Input.mousePosition, OnTileSelected);
        }
    }

    private void OnTileSelected(PlayerScript selectedPlayer, Hex hexSelected)
    {
        selectedPlayer.Ability(hexSelected, AbilityType);
    }
}
