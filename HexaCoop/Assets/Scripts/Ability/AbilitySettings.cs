using System.Collections.Generic;

public class AbilitySetting
{
    public int Cost;
    public AbilityType Type;
    public int AvailableFromTurn;
    public int AvailableFromQueuePlace;
    public bool EventImmediatelyFinished;
    public bool IsPickup;
    public float Duration;
    public bool TargetHexIsRelativeToPlayer;
}