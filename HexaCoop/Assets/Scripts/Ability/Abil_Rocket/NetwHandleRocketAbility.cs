using System.Linq;
using UnityEngine;

public class NetwHandleRocketAbility : HexaEventCallback, IAbilityNetworkHandler
{
    [ComponentInject] private PlayerScript playerScript;

    public void NetworkHandle(PlayerScript playerDoingAbility, Hex target, Hex target2)
    {
        if (playerDoingAbility.IsOnMyNetwork())
        {
            target.SetFogOnHex(false); // local!
        }

        MonoHelper.instance.SummonRocket(target, playerDoingAbility);
    }
}