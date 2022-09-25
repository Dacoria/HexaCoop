
using System.Linq;
using UnityEngine;

public class NetwHandleBombExplosionAbility : HexaEventCallback, IAbilityNetworkHandler
{
    public bool CanDoAbility(PlayerScript playerDoingAbility, Hex target)
    {
        var allBombs = GameObject.FindObjectsOfType<BombScript>();
        return !target.IsObstacle() && !target.HasUnit() && !allBombs.Any(x => x.CurrentHexTile == target);
    }

    public void NetworkHandle(PlayerScript playerDoingAbility, Hex target)
    {
        var bombPrefab = Rsc.GoEnemiesOrObjMap.Single(x => x.Key == "Bomb").Value;
        var bombGo = Instantiate(bombPrefab);
        var bombScript = bombGo.GetComponent<BombScript>();
        bombScript.SetCurrentHexTile(target);

        var startPos = playerDoingAbility.transform.position += new Vector3(0, 0.33f, 0);
        var endPos = target.transform.position + new Vector3(0, 1.33f, 0);        

        var lerpScript = bombGo.AddComponent<LerpMovement>();
        lerpScript.MoveToDestination(
            startPosition: startPos,
            endPosition: endPos,
            duration: 0.5f
        );


    }
}