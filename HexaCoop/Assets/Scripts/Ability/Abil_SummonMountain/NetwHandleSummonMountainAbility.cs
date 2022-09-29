using System.Collections;
using System.Linq;
using UnityEngine;

public class NetwHandleSummonMountainAbility : HexaEventCallback, IAbilityNetworkHandler
{
    public bool CanDoAbility(PlayerScript playerDoingAbility, Hex target, Hex target2)
    {
        return !target.IsObstacle();
    }

    public void NetworkHandle(PlayerScript playerDoingAbility, Hex target, Hex target2)
    {
        target.ChangeHexStructureType(HexStructureType.Mountain);        
        InitMountainAppearFromGround(playerDoingAbility, target);

        if(target.HasUnit(isAlive: true))
        {
            MoveUnitFromMountain(playerDoingAbility, target.GetUnit(isAlive: true));
        }
    }

    private void MoveUnitFromMountain(PlayerScript playerDoingAbility, IUnit unit)
    {
        // deterministisch!
        var hexWithCrystal = FindObjectOfType<CrystalScript>().Hex;
        unit.MoveToNewDestination(
            MonoHelper.instance.GetClosestFreeNeighbour(unit.CurrentHexTile.HexCoordinates, hexWithCrystal.HexCoordinates)
            .GetHex()
        );
    }

    private void InitMountainAppearFromGround(PlayerScript player, Hex target)
    {
        var mountainGo = Utils.GetStructureGoFromHex(target);
        var lerpScript = mountainGo.GetAdd<LerpMovement>();
        lerpScript.AppearFromDown(distance: 1.8f, duration: 1f);

        mountainGo.AddComponent<TemperaryMountainScript>();

        StartCoroutine(ShowTurnsLeftInXSeconds(1f, mountainGo.transform));
    }

    private IEnumerator ShowTurnsLeftInXSeconds(float waitTime, Transform parent)
    {
        yield return Wait4Seconds.Get(waitTime);

        var turnsLeftVisualizerPrefab = Rsc.GoGuiMap.First(x => x.Key == "TurnsLeftVisualizer").Value;
        var turnsLeftGo = Instantiate(turnsLeftVisualizerPrefab, parent);
    }
}