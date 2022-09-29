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

    public IUnit GetUnit(int id) => GetAllUnits(null).First(x => x.Id == id);
    public IUnit GetUnit(Hex hex, bool? isAlive) => GetAllUnits(isAlive).FirstOrDefault(x => x.CurrentHexTile == hex);
    public IUnit GetUnit(Hex hex, UnitType unitType, bool? isAlive) => GetAllUnits(isAlive).FirstOrDefault(x => x.CurrentHexTile == hex && x.UnitType == unitType);    

    public List<IUnit> GetAllUnits(bool? isAlive) =>
        NetworkHelper.instance.GetAllPlayers(isAlive: isAlive).Select(x => (IUnit)x).Concat(
            EnemyManager.instance.GetEnemies().Select(x => (IUnit)x)
        ).ToList();
        
}

public static class Units
{
    public static IUnit GetUnit(this int id) => UnitManager.instance.GetUnit(id);
    public static IUnit GetUnit(this Hex hex, bool? isAlive) => UnitManager.instance.GetUnit(hex, isAlive);
    public static IUnit GetUnit(this Hex hex, UnitType unitType, bool? isAlive) => UnitManager.instance.GetUnit(hex, unitType, isAlive);
    public static bool HasUnit(this Hex hex, bool? isAlive) => hex.GetUnit(isAlive) != null;
    public static PlayerScript GetPlayer(this Hex hex, bool? isAlive) => hex.GetUnit(UnitType.Player, isAlive) as PlayerScript;
    public static EnemyScript GetEnemy(this Hex hex, bool? isAlive) => hex.GetUnit(UnitType.Enemy, isAlive) as EnemyScript;
}