public class PickupItemScript : HexaEventCallback
{
    [ComponentInject] private Hex hex;

    protected override void OnPlayerMovingFinished(PlayerScript player)
    {
        if (player.CurrentHexTile == hex)
        {
            switch (hex.HexObjectOnTileType)
            {
                case HexObjectOnTileType.Artilery_Pickup:
                    player.GetComponent<PlayerAbilityPickups>().AddPickupAbility(AbilityType.Artillery);
                    Destroy(gameObject);
                    break;
            }
        }
    }
}
