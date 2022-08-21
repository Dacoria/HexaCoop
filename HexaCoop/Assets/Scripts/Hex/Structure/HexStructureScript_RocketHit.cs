public partial class HexStructureScript: HexaEventCallback
{
    protected override void OnPlayerRocketHitTile(PlayerScript playerWhoShot, Hex hex)
    {
        if(hex.HexStructureType == HexStructureType.Mountain)
        {
            DestroyMountain(hex);
        }
    }

    private void DestroyMountain(Hex hex)
    {
        var mountainGo = Utils.GetStructureGoFromHex(hex);
        var lerpScript = mountainGo.GetSet<LerpMovement>();
        lerpScript.MoveDown(distance: 1.5f, duration: 2f, callbackOnFinished: () => hex.ChangeHexStructureType(HexStructureType.None));
    }
}