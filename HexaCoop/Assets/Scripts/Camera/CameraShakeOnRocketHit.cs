public class CameraShakeOnRocketHit : HexaEventCallback
{
    protected override void OnPlayerDamageObjectHitTile(PlayerScript player, Hex hexHit, DamageObjectType doType)
    {
        hexHit.EnableHighlight(HighlightColorType.Pink);
        CameraShake.instance.Shake();
        
    }
}
