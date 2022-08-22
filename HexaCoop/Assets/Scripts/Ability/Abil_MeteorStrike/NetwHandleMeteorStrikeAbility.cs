using System.Collections;
using System.Linq;
using UnityEngine;

public class NetwHandleMeteorStrikeAbility : HexaEventCallback, IAbilityNetworkHandler
{
    public void NetworkHandle(PlayerScript playerDoingAbility, Hex selectedHex)
    {
        var allTiles = HexGrid.instance.GetAllTiles();

        playerDoingAbility.gameObject.AddComponent<PlayerFireImmumeScript>();

        allTiles.Shuffle();
        var targets = allTiles.Where(x => x != playerDoingAbility.CurrentHexTile).Take(10);

        var waitTimeToSummonMeteorStrike = 0f;
        
        foreach (var targetHexForMeteor in targets)
        {
            StartCoroutine(MonoHelper.instance.SummonFallingDamageObject(waitTimeToSummonMeteorStrike, targetHexForMeteor, playerDoingAbility, DamageObjectType.MeteorStrike));
            waitTimeToSummonMeteorStrike += 0.3f;
        }
    }
}