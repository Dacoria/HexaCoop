public class PickupItemScript : HexaEventCallback
{
    [ComponentInject] private Hex hexOfPickupItem;

    protected override void OnPlayerMovingFinished(PlayerScript player, Hex hexMovedTo)
    {
        if (hexMovedTo == hexOfPickupItem)
        {
            switch (hexOfPickupItem.HexObjectOnTileType)
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
