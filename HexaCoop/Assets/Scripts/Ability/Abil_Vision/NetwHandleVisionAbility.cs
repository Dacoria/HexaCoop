using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetwHandleVisionAbility : HexaEventCallback, IAbilityNetworkHandler
{
    public void NetworkHandle(PlayerScript playerDoingAbility, Hex target)
    {        
        if (playerDoingAbility.IsMyTurn())
        {
            target.SetFogOnHex(false); // local!
        }

        target.EnableHighlight(HighlightColorType.Yellow);
    }
}