
using System.Linq;
using UnityEngine;

public class NetwHandleBombExplosionAbility : HexaEventCallback, IAbilityNetworkHandler
{
    public bool CanDoAbility(PlayerScript playerDoingAbility, Hex target)
    {
        return !target.IsObstacle() && !target.HasUnit();
    }

    public void NetworkHandle(PlayerScript playerDoingAbility, Hex target)
    {
        var bombPrefab = Rsc.GoEnemiesOrObjMap.Single(x => x.Key == "Bomb").Value;
        var bombScript = bombPrefab.GetComponent<BombScript>();
        bombScript.SetCurrentHexTile(target);

        var damageObjectGo = Instantiate(bombPrefab, target.transform.position + new Vector3(0,1.33f,0), Quaternion.identity);
    }
}