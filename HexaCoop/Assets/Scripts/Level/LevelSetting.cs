using System.Collections.Generic;

public class LevelSetting
{
    public int LevelNr;
    public List<PlayerCountLevelSetting> PlayerCountLevelSettings;
}

public class PlayerCountLevelSetting
{
    public List<int> PlayerCount;
    public List<AbilityType> AvailableAbilities;
    public int HealthPointsForPlayer;
    public int ActionPointsEachTurn;
}