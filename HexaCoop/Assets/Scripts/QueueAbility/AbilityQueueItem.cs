using UnityEngine;

public class AbilityQueueItem
{
    public int Id { get; private set; }
    public PlayerScript Player { get; private set; }
    public Hex Hex { get; private set; }
    public Hex Hex2 { get; private set; }
    public Vector3Int HexCoor { get; private set; }
    public Vector3Int HexCoor2 { get; private set; }
    public AbilityType AbilityType { get; private set; }

    public AbilityQueueItem(PlayerScript player, Hex hex, Hex hex2, AbilityType abilityType, int? id = null)
    {
        Player = player;
        Hex = hex;
        Hex2 = hex2;
        HexCoor = hex.HexCoordinates; // voor queueing icm swap --> referentie naar daadwerkelijke hex
        HexCoor2 = hex2 == null ? Utils.DefaultEmptyV3Int : hex2.HexCoordinates; // voor queueing icm swap --> referentie naar daadwerkelijke hex
        AbilityType = abilityType;
        Id = id.HasValue ? id.Value : MonoHelper.instance.GenerateNewId();
    }

    // voor swapping --> refresh waardes
    public void UpdateHexByCoor()
    {
        Hex = HexCoor.GetHex();
        Hex2 = HexCoor2.GetHex();
    }
}