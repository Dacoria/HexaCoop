public class AbilityQueueItem
{
    public int Id;
    public PlayerScript Player;
    public Hex Hex;
    public AbilityType AbilityType;

    public AbilityQueueItem(PlayerScript player, Hex hex, AbilityType abilityType)
    {
        Player = player;
        Hex = hex;
        AbilityType = abilityType;
        Id = MonoHelper.instance.GenerateNewId();
    }    
}