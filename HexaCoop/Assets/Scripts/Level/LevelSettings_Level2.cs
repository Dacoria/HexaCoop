using System.Collections.Generic;

public static partial class LevelSettings
{
    public static LevelSetting Level2 = new LevelSetting
    {
        LevelNr = 2,
        PlayerCountLevelSettings = new List<PlayerCountLevelSetting> {
            new PlayerCountLevelSetting
            {
                PlayerCount = new List<int>{1,2},
                ActionPointsEachTurn = 4,
                HealthPointsForPlayer = 2,
                AvailableAbilities = new List<AbilityType>
                {
                    AbilityType.Movement,
                    AbilityType.Rocket,
                    AbilityType.Wait,
                    AbilityType.SummonMountain,
                    AbilityType.Artillery,
                    AbilityType.MeteorStrike
                }
            },
            new PlayerCountLevelSetting
            {
                PlayerCount = new List<int>{3,4},
                ActionPointsEachTurn = 4,
                HealthPointsForPlayer = 1,
                AvailableAbilities = new List<AbilityType>
                {
                    AbilityType.Movement,
                    AbilityType.Rocket,
                    AbilityType.Wait,
                    AbilityType.SummonMountain,
                    AbilityType.Artillery,
                    AbilityType.MeteorStrike
                }
            }

        }
    };
}

