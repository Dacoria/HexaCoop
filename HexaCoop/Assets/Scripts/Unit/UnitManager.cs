using Photon.Pun;
using System.Collections;
using UnityEngine;
using System.Linq;
using System;
using System.Collections.Generic;

public class UnitManager : MonoBehaviour
{
    public static UnitManager instance;    

    void Awake()
    {
        instance = this;
    }

    public IUnit GetUnit(int id) => GetAllUnits().First(x => x.Id == id);

    public List<IUnit> GetAllUnits() =>
        (List<IUnit>)NetworkHelper.instance.GetAllPlayers().Concat(
            EnemyManager.instance.GetEnemies().Select(x => (IUnit)x).ToList()
        );
}

public static class Units
{
    public static IUnit GetUnit(this int id) => UnitManager.instance.GetUnit(id);
    public static List<IUnit> GetAllUnits() => UnitManager.instance.GetAllUnits();
}
