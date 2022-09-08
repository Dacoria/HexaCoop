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
                    PickupAbility(player, hexMovedTo, AbilityType.Artillery);
                    break;
                case HexObjectOnTileType.MeteorStrike_Pickup:
                    PickupAbility(player, hexMovedTo, AbilityType.MeteorStrike);                    
                    break;
            }
        }
    }

    private void PickupAbility(PlayerScript player, Hex hexMovedTo, AbilityType ability)
    {
        player.GetComponent<PlayerAbilityPickups>().AddPickupAbility(ability);
        ActionEvents.AbilityPickedUp?.Invoke(player, hexMovedTo, ability);

        Destroy(gameObject);
    }
}
