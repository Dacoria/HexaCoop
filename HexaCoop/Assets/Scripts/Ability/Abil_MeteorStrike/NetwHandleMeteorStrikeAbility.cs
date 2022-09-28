using System.Collections;
using System.Linq;
using UnityEngine;

public class NetwHandleMeteorStrikeAbility : HexaEventCallback, IAbilityNetworkHandler
{
    public void NetworkHandle(PlayerScript playerDoingAbility, Hex selectedHex, Hex target2)
    {
        var allTiles = HexGrid.instance.GetAllTiles();
        var allNonWaterTiles = allTiles.Where(x => !x.HexSurfaceType.IsObstacle()).ToList();

        allNonWaterTiles.Shuffle();
        var targets = allNonWaterTiles.Where(x => x != playerDoingAbility.CurrentHexTile).Take(allNonWaterTiles.Count / 3);

        var waitTimeToSummonMeteorStrike = 0f;
        
        foreach (var targetHexForMeteor in targets)
        {
            StartCoroutine(MonoHelper.instance.SummonFallingDamageObject(waitTimeToSummonMeteorStrike, targetHexForMeteor, playerDoingAbility, DamageObjectType.MeteorStrike));
            waitTimeToSummonMeteorStrike += 0.2f;
        }
    }
}