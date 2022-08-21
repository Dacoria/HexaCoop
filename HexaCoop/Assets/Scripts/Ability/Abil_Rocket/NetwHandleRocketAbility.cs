using System.Linq;
using UnityEngine;

public class NetwHandleRocketAbility : HexaEventCallback, IAbilityNetworkHandler
{
    [ComponentInject] private PlayerScript playerScript;

    public void NetworkHandle(PlayerScript playerDoingAbility, Hex target)
    {
        if (playerDoingAbility.IsOnMyNetwork())
        {
            target.SetFogOnHex(false); // local!
        }

        MonoHelper.instance.SummonRocket(target, playerDoingAbility);
    }
}