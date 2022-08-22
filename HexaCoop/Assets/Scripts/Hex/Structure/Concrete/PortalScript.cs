using System.Collections;
using System.Linq;
using UnityEngine;

public class PortalScript : HexaEventCallback, IStructure
{
    public PortalScript ConnectedPortal;

    [ComponentInject] public Hex Hex;

    void Start()
    {
        ConnectedPortal = FindObjectsOfType<PortalScript>().Where(x => x != this).First();
    }

    public void PlayerSteppedOnStructure(PlayerScript player) => StartCoroutine(PortalToNewHex(player)); // coroutine voorkomt dat je direct door de andere portal weer terugkomt

    private IEnumerator PortalToNewHex(PlayerScript player)
    {
        yield return Wait4Seconds.Get(0.05f);
        if (ConnectedPortal.Hex.HasUnit())
        {
            MoveUnitAwayFromPortal(player, ConnectedPortal.Hex.GetUnit());
        }

        player.transform.position = ConnectedPortal.Hex.transform.position;
        player.SetCurrentHexTile(ConnectedPortal.Hex);

        ActionEvents.PlayerScriptHasTeleported?.Invoke(player, player.CurrentHexTile);        
    }

    private void MoveUnitAwayFromPortal(PlayerScript playerDoingAbility, IUnit unit)
    {
        unit.MoveToNewDestination(
            MonoHelper.instance.GetClosestFreeNeighbour(unit.CurrentHexTile.HexCoordinates, playerDoingAbility.CurrentHexTile.HexCoordinates)
            .GetHex()
        );
    }

    // altijd visible lijk me
    public void SetIsVisible(bool isVisible) { }
}
