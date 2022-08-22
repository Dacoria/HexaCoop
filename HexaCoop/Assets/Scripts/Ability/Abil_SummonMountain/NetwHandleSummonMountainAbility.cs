using System.Linq;
using UnityEngine;

public class NetwHandleSummonMountainAbility : HexaEventCallback, IAbilityNetworkHandler
{
    [ComponentInject] private PlayerScript playerScript;

    public void NetworkHandle(PlayerScript playerDoingAbility, Hex target)
    {
        target.ChangeHexStructureType(HexStructureType.Mountain);        
        InitMountainAppearFromGround(playerDoingAbility, target);

        if(target.HasUnit())
        {
            MoveUnitFromMountain(playerDoingAbility, target.GetUnit());
        }
    }

    private void MoveUnitFromMountain(PlayerScript playerDoingAbility, IUnit unit)
    {
        // deterministisch!
        unit.MoveToNewDestination(
            MonoHelper.instance.GetClosestFreeNeighbour(unit.CurrentHexTile.HexCoordinates, playerDoingAbility.CurrentHexTile.HexCoordinates)
            .GetHex()
        );
    }

    private void InitMountainAppearFromGround(PlayerScript player, Hex target)
    {
        var mountainGo = Utils.GetStructureGoFromHex(target);
        var lerpScript = mountainGo.GetSet<LerpMovement>();
        lerpScript.AppearFromDown(distance: 1.8f, duration: 1f);

        var temperaryMountainScript = target.gameObject.AddComponent<TemperaryMountainScript>();
        temperaryMountainScript.PlayerThatSummonedMountain = player;
    }
}