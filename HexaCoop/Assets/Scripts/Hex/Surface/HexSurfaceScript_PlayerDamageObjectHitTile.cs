using UnityEngine;

public partial class HexSurfaceScript : HexaEventCallback
{
    protected override void OnPlayerDamageObjectHitTile(PlayerScript player, Hex hex, DamageObjectType doType)
    {
        if(doType == DamageObjectType.MeteorStrike)
        {
            hex.ChangeHexSurfaceType(HexSurfaceType.Magma);
        }
    }
}