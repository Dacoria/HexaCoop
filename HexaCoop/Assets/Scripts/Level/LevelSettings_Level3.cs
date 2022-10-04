using System.Collections.Generic;

public static partial class LevelSettings
{
    public static LevelSetting Level3 = new LevelSetting
    {
        LevelNr = 3,
        PlayerCountLevelSettings = new List<PlayerCountLevelSetting> {
            new PlayerCountLevelSetting
            {
                PlayerCount = new List<int>{1},
                ActionPointsEachTurn = 6,
                HealthPointsForPlayer = 2,
                AvailableAbilities = new List<AbilityType>
                {
                    AbilityType.Movement,
                    AbilityType.Rocket,
                    AbilityType.Wait,
                    AbilityType.SummonMountain,
                    AbilityType.BombExplosion,
                    AbilityType.Jump,
                    AbilityType.Artillery,
                    AbilityType.MeteorStrike
                }
            },
            new PlayerCountLevelSetting
            {
                PlayerCount = new List<int>{3,4},
                ActionPointsEachTurn = 6,
                HealthPointsForPlayer = 1,
                AvailableAbilities = new List<AbilityType>
                {
                    AbilityType.Movement,
                    AbilityType.Rocket,
                    AbilityType.Wait,
                    AbilityType.SummonMountain,
                    AbilityType.BombExplosion,
                    AbilityType.Jump,
                    AbilityType.Artillery,
                    AbilityType.MeteorStrike
                }
            }

        }
    };
}

