public class CameraShakeOnRocketHit : HexaEventCallback
{
    protected override void OnPlayerRocketHitTile(PlayerScript playerThatSendRocket, Hex hexHit)
    {     
        hexHit.EnableHighlight(HighlightColorType.Pink);
        CameraShake.instance.Shake();
        
    }
}
