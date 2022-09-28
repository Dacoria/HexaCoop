public class AbilityQueueItem
{
    public int Id { get; private set; }
    public PlayerScript Player { get; private set; }
    public Hex Hex { get; private set; }
    public Hex Hex2 { get; private set; }
    public AbilityType AbilityType { get; private set; }

    public AbilityQueueItem(PlayerScript player, Hex hex, Hex hex2, AbilityType abilityType, int? id = null)
    {
        Player = player;
        Hex = hex;
        Hex2 = hex2;
        AbilityType = abilityType;
        Id = id.HasValue ? id.Value : MonoHelper.instance.GenerateNewId();
    }    
}