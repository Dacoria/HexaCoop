using System.Collections.Generic;
using System.Linq;

public partial class HexStructureScript: HexaEventCallback
{
    protected override void OnBombExplosionHit(List<Hex> tiles, int damage)
    {
        if (tiles.Any(x => x== hex))
        {
            switch (hex.HexStructureType)
            {
                case HexStructureType.Mountain:
                    DestroyMountain(hex);
                    break;                
            }
        }
    }
}
