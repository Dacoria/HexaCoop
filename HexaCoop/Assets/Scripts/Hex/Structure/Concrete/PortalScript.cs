using System.Collections;
using System.Linq;
using UnityEngine;

public class PortalScript : HexaEventCallback, IStructure
{
    public Vector3Int HexCoorConnectedPortal;

    private PortalScript ConnectedPortal;
    [HideInInspector] [ComponentInject] private Hex hex;

    void Start()
    {
        if(ConnectedPortal == null)
        {
            if (HexCoorConnectedPortal.GetHex() != null)
            {
                ConnectedPortal = HexCoorConnectedPortal.GetHex().GetComponentInChildren<PortalScript>();
            }
            else
            {
                ConnectedPortal = FindObjectsOfType<PortalScript>().Where(x => x != this).OrderByDescending(x => Vector3.Distance(x.transform.position, this.transform.position)).First();
            }
        }

        
    }

    public void PlayerSteppedOnStructure(PlayerScript player) => StartCoroutine(PortalToNewHex(player)); // coroutine voorkomt dat je direct door de andere portal weer terugkomt

    private IEnumerator PortalToNewHex(PlayerScript player)
    {
        yield return Wait4Seconds.Get(0.05f);
        if (ConnectedPortal.hex.HasUnit())
        {
            MoveUnitAwayFromPortal(player, ConnectedPortal.hex.GetUnit());
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

    // altijd visible lijk me
    public void SetIsVisible(bool isVisible) { }
}
