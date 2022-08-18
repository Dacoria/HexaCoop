using System.Collections.Generic;

public enum AbilityType
{
    None,
    Rocket,
    Movement,
    Radar,
    Vision,
    BearTrap,
    Binocular,

    Meditate,
    Forcefield,
}

public static class AbilitySetup
{
    public static List<AbilitySetting> AbilitySettings = new List<AbilitySetting>
    {
        new AbilitySetting{Type = AbilityType.Rocket,           Cost = 3,   AvailableFromTurn = 2, EventImmediatelyFinished = false},
        new AbilitySetting{Type = AbilityType.Movement,         Cost = 2,   AvailableFromTurn = 1, EventImmediatelyFinished = false},
        new AbilitySetting{Type = AbilityType.Radar,            Cost = 1,   AvailableFromTurn = 1, EventImmediatelyFinished = true},
        new AbilitySetting{Type = AbilityType.Vision,           Cost = 1,   AvailableFromTurn = 1, EventImmediatelyFinished = true},
        new AbilitySetting{Type = AbilityType.BearTrap,         Cost = 2,   AvailableFromTurn = 2, EventImmediatelyFinished = true},
        new AbilitySetting{Type = AbilityType.Binocular,        Cost = 2,   AvailableFromTurn = 1, EventImmediatelyFinished = true},

        new AbilitySetting{Type = AbilityType.Meditate,         Cost = 0,   AvailableFromTurn = 999, EventImmediatelyFinished = true},
        new AbilitySetting{Type = AbilityType.Forcefield,       Cost = 2,   AvailableFromTurn = 999, EventImmediatelyFinished = true},
    };
}