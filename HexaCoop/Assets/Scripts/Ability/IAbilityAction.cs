public interface IAbilityAction
{
    bool CanDoAbility(PlayerScript player);
    void InitAbilityAction();
    void DeselectAbility();
    AbilityType AbilityType { get; }
}