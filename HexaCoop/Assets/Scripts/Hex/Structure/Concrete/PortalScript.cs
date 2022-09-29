using System.Collections;
using System.Linq;
using UnityEngine;

public class PortalScript : HexaEventCallback, IStructure
{
    private PortalScript ConnectedPortal;
    [HideInInspector] [ComponentInject] private Hex hex;

    void Start()
    {
        ConnectedPortal = FindObjectsOfType<PortalScript>().Where(x => x != this).OrderByDescending(x => Vector3.Distance(x.transform.position, this.transform.position)).First();
    }

    public void PlayerSteppedOnStructure(PlayerScript player) => StartCoroutine(PortalToNewHex(player)); // coroutine voorkomt dat je direct door de andere portal weer terugkomt

    private IEnumerator PortalToNewHex(PlayerScript player)
    {
        yield return Wait4Seconds.Get(0.05f);
        if (ConnectedPortal.hex.HasUnit(isAlive: true))
        {
            MoveUnitAwayFromPortal(player, ConnectedPortal.hex.GetUnit(isAlive: true));
        }

        player.transform.position = ConnectedPortal.hex.transform.position;
        player.SetCurrentHexTile(ConnectedPortal.hex);

        ActionEvents.PlayerHasTeleported?.Invoke(player, player.CurrentHexTile);
    }

    private void MoveUnitAwayFromPortal(PlayerScript playerDoingAbility, IUnit unit)
    {
        unit.MoveToNewDestination(
            MonoHelper.instance.GetClosestFreeNeighbour(unit.CurrentHexTile.HexCoordinates, playerDoingAbility.CurrentHexTile.HexCoordinates)
            .GetHex()
        );
    }

    // altijd visible lijkt me
    public void SetIsVisible(bool isVisible) { }
}
