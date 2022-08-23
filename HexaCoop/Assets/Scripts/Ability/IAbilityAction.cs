public interface IAbilityAction
{
    bool CanDoAbility(PlayerScript player)
    {
        if(AbilityType.IsPickup())
        {
            return player?.GetComponent<PlayerAbilityPickups>()?.HasPickupAbility(AbilityType) == true;
        }

        return true;        
    }
    void InitAbilityAction();
    void DeselectAbility();
    AbilityType AbilityType { get; }
}