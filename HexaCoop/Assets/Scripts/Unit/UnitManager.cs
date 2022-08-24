using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;    

    void Awake()
    {
        instance = this;
    }

    public IUnit GetUnit(int id) => GetAllUnits().First(x => x.Id == id);
    public IUnit GetUnit(Hex hex) => GetAllUnits().FirstOrDefault(x => x.CurrentHexTile == hex);
    public IUnit GetUnit(Hex hex, UnitType unitType) => GetAllUnits().FirstOrDefault(x => x.CurrentHexTile == hex && x.UnitType == unitType);

    public List<IUnit> GetAllUnits() =>
        NetworkHelper.instance.GetAllPlayers().Select(x => (IUnit)x).Concat(
            EnemyManager.instance.GetEnemies().Select(x => (IUnit)x)
        ).ToList();
        
}

public static class Units
{
    public static IUnit GetUnit(this int id) => UnitManager.instance.GetUnit(id);
    public static IUnit GetUnit(this Hex hex) => UnitManager.instance.GetUnit(hex);
    public static IUnit GetUnit(this Hex hex, UnitType unitType) => UnitManager.instance.GetUnit(hex, unitType);
    public static bool HasUnit(this Hex hex) => hex.GetUnit() != null;
    public static PlayerScript GetPlayer(this Hex hex) => hex.GetUnit(UnitType.Player) as PlayerScript;
    public static EnemyScript GetEnemy(this Hex hex) => hex.GetUnit(UnitType.Enemy) as EnemyScript;
}