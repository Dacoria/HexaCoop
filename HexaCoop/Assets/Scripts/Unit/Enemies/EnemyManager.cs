using Photon.Pun;
using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    public List<EnemyScript> GetEnemies(bool? isAlive = null)
    {
        var enemies = GetEnemies();
        if(isAlive.HasValue)
        {
            enemies = enemies.Where(x => x.IsAlive == isAlive.Value).ToList();
        }

        return enemies;
    }

    private List<EnemyScript> GetEnemies() => 
        ObjectNetworkInitManager.instance.SpawnedNetworkObjects
        .Select(x => x.Value)
        .Where(x => x.activeSelf)
        .Where(x => x.GetComponent<EnemyScript>() != null)
        .Where(x => x.GetComponent<EnemyHealth>().CurrentHitPoints > 0)
        .Select(x => x.GetComponent<EnemyScript>())
        .ToList();

    void Awake()
    {
        instance = this;
    }

    private IEnumerator Start()
    {
        
        yield return Wait4Seconds.Get(0.15f); // wachten tot hexgrid geladen wordt -> nodig om huidige hex tile te krijgen....
        if (PhotonNetwork.IsMasterClient)
        {
            ReplaceEnemiesWithPunEnemies();
        }
        else
        {
            DestroyLocalEnemies();
        }
    }

    private void DestroyLocalEnemies()
    {
        var enemies = GameObject.FindObjectsOfType<EnemyScript>().ToList();
        foreach (EnemyScript enemy in enemies.Where(x => x.GetComponent<SyncedMetaData>().Id == 0))
        {
            Destroy(enemy.gameObject);
        }
    }

    private void ReplaceEnemiesWithPunEnemies()
    {
        var enemies = GameObject.FindObjectsOfType<EnemyScript>().ToList();

        foreach (EnemyScript enemy in enemies)
        {
            ObjectNetworkInitManager.instance.InstantiatePunObject(enemy.gameObject);            
            Destroy(enemy.gameObject);
        }
    }
}
