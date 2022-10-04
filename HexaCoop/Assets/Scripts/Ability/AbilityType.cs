using System.Collections.Generic;
using System.Collections.ObjectModel;

public enum AbilityType
{
    None,
    Rocket,
    Movement,
    Jump,
    Wait,
    BombExplosion,
    SwapTiles,

    BearTrap,

    SummonMountain,

    Meditate,
    Forcefield,
    Artillery,
    MeteorStrike,
}

public static class AbilitySetup
{
    public static List<AbilitySetting> AbilitySettings = new List<AbilitySetting>
    {
        new AbilitySetting{Type = AbilityType.Rocket,         Cost = 2,   AvailableFromTurn = 1,   EventImmediatelyFinished = false, Duration = 2.0f, IsPickup = false, AvailableFromQueuePlace = 1, TargetHexIsRelativeToPlayer = false},
        new AbilitySetting{Type = AbilityType.Movement,       Cost = 2,   AvailableFromTurn = 1,   EventImmediatelyFinished = false, Duration = 2.3f, IsPickup = false, AvailableFromQueuePlace = 0, TargetHexIsRelativeToPlayer = true },
        new AbilitySetting{Type = AbilityType.Jump,           Cost = 4,   AvailableFromTurn = 1,   EventImmediatelyFinished = false, Duration = 2.3f, IsPickup = false, AvailableFromQueuePlace = 0, TargetHexIsRelativeToPlayer = true },
        new AbilitySetting{Type = AbilityType.Artillery,      Cost = 0,   AvailableFromTurn = 1,   EventImmediatelyFinished = true,  Duration = 2.3f, IsPickup = true , AvailableFromQueuePlace = 1, TargetHexIsRelativeToPlayer = true },
        new AbilitySetting{Type = AbilityType.MeteorStrike,   Cost = 0,   AvailableFromTurn = 1,   EventImmediatelyFinished = true,  Duration = 3.5f, IsPickup = true , AvailableFromQueuePlace = 1, TargetHexIsRelativeToPlayer = false},
        new AbilitySetting{Type = AbilityType.Wait,           Cost = 0,   AvailableFromTurn = 1,   EventImmediatelyFinished = true,  Duration = 1.8f, IsPickup = false, AvailableFromQueuePlace = 0, TargetHexIsRelativeToPlayer = false},
        new AbilitySetting{Type = AbilityType.SwapTiles,      Cost = 3,   AvailableFromTurn = 1,   EventImmediatelyFinished = true,  Duration = 1.8f, IsPickup = false, AvailableFromQueuePlace = 0, TargetHexIsRelativeToPlayer = false},
        new AbilitySetting{Type = AbilityType.SummonMountain, Cost = 2,   AvailableFromTurn = 1,   EventImmediatelyFinished = true,  Duration = 2.3f, IsPickup = false, AvailableFromQueuePlace = 0, TargetHexIsRelativeToPlayer = false},
        new AbilitySetting{Type = AbilityType.Forcefield,     Cost = 1,   AvailableFromTurn = 1,   EventImmediatelyFinished = true,  Duration = 2.0f, IsPickup = false, AvailableFromQueuePlace = 0, TargetHexIsRelativeToPlayer = false},
        new AbilitySetting{Type = AbilityType.BombExplosion,  Cost = 2,   AvailableFromTurn = 1,   EventImmediatelyFinished = true,  Duration = 1.5f, IsPickup = false, AvailableFromQueuePlace = 0, TargetHexIsRelativeToPlayer = true },
                                                                                                                                                                                                                                        
                                                                                                                                                                                                                                        
        new AbilitySetting{Type = AbilityType.BearTrap,       Cost = 2,   AvailableFromTurn = 999, EventImmediatelyFinished = true,  Duration = 2.0f, IsPickup = false, AvailableFromQueuePlace = 0, TargetHexIsRelativeToPlayer = false},
        new AbilitySetting{Type = AbilityType.Meditate,       Cost = 0,   AvailableFromTurn = 999, EventImmediatelyFinished = true, Duration = 2.0f, IsPickup = false, AvailableFromQueuePlace = 0, TargetHexIsRelativeToPlayer = false },
    };
}