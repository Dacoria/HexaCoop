using System.Collections;
using System.Linq;
using UnityEngine;

public class NetwHandleSummonMountainAbility : HexaEventCallback, IAbilityNetworkHandler
{
    public bool CanDoAbility(PlayerScript playerDoingAbility, Hex target)
    {
        return !target.IsObstacle();
    }

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
        var lerpScript = mountainGo.GetAdd<LerpMovement>();
        lerpScript.AppearFromDown(distance: 1.8f, duration: 1f);

        var temperaryMountainScript = target.gameObject.AddComponent<TemperaryMountainScript>();
        temperaryMountainScript.PlayerThatSummonedMountain = player;

        StartCoroutine(ShowTurnsLeftInXSeconds(1f, target.gameObject.transform));
    }

    private IEnumerator ShowTurnsLeftInXSeconds(float waitTime, Transform parent)
    {
        yield return Wait4Seconds.Get(waitTime);

        var turnsLeftVisualizerPrefab = Rsc.GoGuiMap.First(x => x.Key == "TurnsLeftVisualizer").Value;
        var turnsLeftGo = Instantiate(turnsLeftVisualizerPrefab, parent);
    }
}