public class AbilityQueueItem
{
    public int Id { get; private set; }
    public PlayerScript Player { get; private set; }
    public Hex Hex { get; private set; }
    public AbilityType AbilityType { get; private set; }

    public AbilityQueueItem(PlayerScript player, Hex hex, AbilityType abilityType, int? id = null)
    {
        Player = player;
        Hex = hex;
        AbilityType = abilityType;
        Id = id.HasValue ? id.Value : MonoHelper.instance.GenerateNewId();
    }    
}