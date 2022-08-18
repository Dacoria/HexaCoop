using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyScript : HexaEventCallback, ICurrentHex
{
    [ComponentInject] private SyncedMetaData syncedMetaData;

    private GameObject modelGo;

    private new void Awake()
    {
        base.Awake();
        gameObject.AddComponent<EnemyHealth>();
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
    public Hex CurrentHexTile;   

    private Action callbackOnFinished;

    public Hex GetCurrentHexTile() => CurrentHexTile;
    public void SetCurrentHexTile(Hex hex) => CurrentHexTile = hex;

    public void DoAction(Action callbackOnFinished)
    {
        this.callbackOnFinished = callbackOnFinished;

        var closestPlayer = NetworkHelper.instance.ClosestPlayer(transform.position);
        var fastestPathToPlayer = GetPathToHex(CurrentHexTile.HexCoordinates, closestPlayer.CurrentHexTile.HexCoordinates);

        if(fastestPathToPlayer.Count == 1)
        {
            NetworkActionEvents.instance.EnemyAttack(this, closestPlayer);
        }
        else
        {
            var randomNumber = UnityEngine.Random.Range(0, 2);
            if(randomNumber == 0 && !fastestPathToPlayer.First().GetHex().HasUnitOnHex())
            {
                NetworkActionEvents.instance.EnemyMove(this, fastestPathToPlayer.First().GetHex());
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
}
