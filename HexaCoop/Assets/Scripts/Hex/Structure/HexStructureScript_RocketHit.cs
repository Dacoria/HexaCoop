public partial class HexStructureScript: HexaEventCallback
{
    protected override void OnPlayerDamageObjectHitTile(PlayerScript playerOwner, Hex hexTileHit, DamageObjectType doType)
    {
        if (hex == hexTileHit)
        {
            if (hex.HexStructureType == HexStructureType.Mountain)
            {
                hex.DestroyStructure();
            }
        }
    }    
}