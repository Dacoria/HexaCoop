using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyScript : HexaEventCallback, IUnit
{
    [ComponentInject] private SyncedMetaData syncedMetaData;
    private EnemyHealth enemyHealth;

    private GameObject modelGo;

    private new void Awake()
    {
        base.Awake();
        this.enemyHealth = gameObject.AddComponent<EnemyHealth>();

        gameObject.AddComponent<EnemyAttack>();
        gameObject.AddComponent<EnemyMovement>();
        gameObject.AddComponent<TooltipEnemy>();
        modelGo = transform.GetChild(0).gameObject; // aanname voor nu: Child van enemy is altijd Model!

        if (Settings.ShowOnlyPlainHexesBeforeGameStart) 
        { 
            SetVisible(false); 
        }
    }

    public int Id => syncedMetaData.Id;
    public Hex CurrentHexTile { get; private set; }
    public UnitType UnitType => UnitType.Enemy;
    public bool IsAlive => enemyHealth.CurrentHitPoints > 0;

    private Action callbackOnFinished;
    public void SetCurrentHexTile(Hex hex) => CurrentHexTile = hex;

    public void DoAction(Action callbackOnFinished)
    {
        this.callbackOnFinished = callbackOnFinished;

        var closestPlayer = NetworkHelper.instance.ClosestPlayer(transform.position);
        var fastestPathToPlayer = GetPathToHex(CurrentHexTile.HexCoordinates, closestPlayer.CurrentHexTile.HexCoordinates);

        if(fastestPathToPlayer.Count == 1)
        {
            NetworkAE.instance.EnemyAttack(this, closestPlayer);
        }
        else
        {
            var randomNumber = UnityEngine.Random.Range(0, 2);
            if(randomNumber == 0 && !fastestPathToPlayer.First().GetHex().HasUnit())
            {
                NetworkAE.instance.EnemyMove(this, fastestPathToPlayer.First().GetHex());
            }
            else
            {
                // geen actie
                ActionFinished();
            }
            
        }        
    }

    public void ActionFinished()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            callbackOnFinished();
        }
    }

    private List<Vector3Int> GetPathToHex(Vector3Int from, Vector3Int to)
    {
        var astar = new AStarSearch(from, to);
        var path = astar.FindPath();
        return path;
    }

    public void SetVisible(bool isVisible) => modelGo.SetActive(isVisible);
    public void MoveToNewDestination(Hex tile) => gameObject.GetSet<UnitMovement>().GoToDestination(tile, 1);
}
