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
        Netw.CurrPlayer().Ability(Netw.CurrPlayer().CurrentHexTile, AbilityType);
    }

    private void Update()
    {
        if (!abilIsActive)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            NeighbourHexTileSelectionManager.instance.HandleMouseClickForMove(Input.mousePosition, OnMovementTileSelected);
        }
    }

    private void OnMovementTileSelected(Hex hexSelected)
    {
        Netw.CurrPlayer().Ability(hexSelected, AbilityType);
    }
}