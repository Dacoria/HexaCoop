public class AbilityQueueItem
{
    public int Id { get; private set; }
    public PlayerScript Player { get; private set; }
    public Hex Hex { get; private set; }
    public AbilityType AbilityType { get; private set; }

    public AbilityQueueItem(PlayerScript player, Hex hex, AbilityType abilityType)
    {
        Player = player;
        Hex = hex;
        AbilityType = abilityType;
        Id = MonoHelper.instance.GenerateNewId();
    }    
}