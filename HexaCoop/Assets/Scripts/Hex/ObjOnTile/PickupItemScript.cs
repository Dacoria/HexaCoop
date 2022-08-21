public class PickupItemScript : HexaEventCallback
{
    [ComponentInject] private Hex hex;

    protected override void OnPlayerMovingFinished(PlayerScript player)
    {
        if (player.CurrentHexTile == hex)
        {
            switch (hex.HexObjectOnTileType)
            {
                case HexObjectOnTileType.Artillery_Pickup:
                    player.GetComponent<PlayerAbilityPickups>().AddPickupAbility(AbilityType.Artillery);
                    Destroy(gameObject);
                    break;
                case HexObjectOnTileType.MeteorStrike_Pickup:
                    player.GetComponent<PlayerAbilityPickups>().AddPickupAbility(AbilityType.MeteorStrike);
                    Destroy(gameObject);
                    break;
            }
        }
    }
}
