using UnityEngine;

public partial class HexSurfaceScript : HexaEventCallback
{
    protected override void OnAllPlayersFinishedTurn()
    {
        switch(hex.HexSurfaceType)
        {
            case HexSurfaceType.Magma:
                hex.ChangeHexSurfaceType(HexSurfaceType.Lava_Stones);
                break;
            case HexSurfaceType.Lava_Stones:
                hex.ChangeHexSurfaceType(HexSurfaceType.Lava_Stones_Remains);
                break;
        }
    }
}