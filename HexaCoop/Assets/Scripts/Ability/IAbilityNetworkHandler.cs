public interface IAbilityNetworkHandler
{
    public bool CanDoAbility(PlayerScript playerDoingAbility, Hex target)
    {
        return true;
    }

    public void NetworkHandle(PlayerScript playerDoingAbility, Hex target);
}