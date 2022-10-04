using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Linq;

public static partial class LevelSettings
{
    private static List<LevelSetting> _levelSettings;
    private static List<LevelSetting> AllLevelSettings
    {
        get
        {
            if(_levelSettings == null )
            {
                _levelSettings = new List<LevelSetting>
                {
                    Level1,
                    Level2,
                    Level3
                };
            };

            return _levelSettings;
        }
    }

    public static PlayerCountLevelSetting GetCurrLevelSettings()
    {
        var levelNr = SceneManager.GetActiveScene().name.GetLevelNr();
        var allPlayersCount = NetworkHelper.instance.GetAllPlayers().Count();

        foreach(var levelSetting in AllLevelSettings)
        {
            if(levelSetting.LevelNr != levelNr)
            {
                continue;
            }

            foreach (var playerCountLevelSettings in levelSetting.PlayerCountLevelSettings)
            {
                if(playerCountLevelSettings.PlayerCount.Any(x => x == allPlayersCount))
                {
                    return playerCountLevelSettings;
                }
            }
        }

        throw new System.Exception("Geen setting gevonden");
    }

    public static int GetHealthPointsForPlayer() => GetCurrLevelSettings().HealthPointsForPlayer;
    public static int GetActionPointsEachTurn() => GetCurrLevelSettings().ActionPointsEachTurn;
    public static bool IsAvailableInLevel(AbilityType abilType) => GetCurrLevelSettings().AvailableAbilities.Any(x => x == abilType);
}