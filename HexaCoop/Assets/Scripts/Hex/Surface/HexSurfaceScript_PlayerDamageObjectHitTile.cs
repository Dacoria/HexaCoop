using UnityEngine;

public partial class HexSurfaceScript : HexaEventCallback
{
    protected override void OnPlayerDamageObjectHitTile(PlayerScript player, Hex hexTileHit, DamageObjectType doType)
    {
        if (hex == hexTileHit)
        {
            if (doType == DamageObjectType.MeteorStrike)
            {
                hex.ChangeHexSurfaceType(HexSurfaceType.Magma);
            }
        }
    }
}