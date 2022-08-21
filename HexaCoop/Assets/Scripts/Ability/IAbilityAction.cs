public interface IAbilityAction
{
    bool CanDoAbility(PlayerScript player)
    {
        if(AbilityType.IsPickup())
        {
            return player.GetComponent<PlayerAbilityPickups>().HasPickupAbility(AbilityType);
        }

        return true;        
    }
    void InitAbilityAction();
    void DeselectAbility();
    AbilityType AbilityType { get; }
}