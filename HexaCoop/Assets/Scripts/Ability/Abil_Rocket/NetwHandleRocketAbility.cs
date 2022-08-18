using System.Linq;
using UnityEngine;

public class NetwHandleRocketAbility : HexaEventCallback, IAbilityNetworkHandler
{
    [ComponentInject] private PlayerScript playerScript;

    public void NetworkHandle(PlayerScript playerDoingAbility, Hex target)
    {
        if (playerDoingAbility.IsMyTurn())
        {
            target.SetFogOnHex(false); // local!
        }

        Vector3 destination = target.transform.position + new Vector3(0, 15, 0);
        var rocketPrefab = Rsc.GoEnemiesOrObjMap.Single(x => x.Key == "Rocket").Value;
        var rocketGo = Instantiate(rocketPrefab, destination, Quaternion.Euler(0, 0, 180f));
        rocketGo.GetComponent<RocketScript>().Player = playerScript;
        rocketGo.GetComponent<RocketScript>().HexTarget = target;
    }

    protected override void OnPlayerRocketHitTile(PlayerScript playerThatSendRocket, Hex hexHit)
    {
        if (playerScript == playerThatSendRocket)
        {
            hexHit.EnableHighlight(HighlightColorType.Pink);
            CameraShake.instance.Shake();

            // HP checks (en evt dood gaan) gebeuren in het health script vd player
        }
    }
}