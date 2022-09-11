using System.Collections.Generic;
using UnityEngine;

public class PlayerForcefieldScript : HexaEventCallback
{
    [ComponentInject] private PlayerScript playerScript;

    public int CurrentHitPoints;
    public int InitHitPoints;

    private int TurnActivated;
    private GameObject forcefieldGo;

    private new void Awake()
    {
        base.Awake();
        InitHitPoints = 1;
        CurrentHitPoints = InitHitPoints;
        TurnActivated = GameHandler.instance.CurrentTurn;

        var playerModel = GetComponentInChildren<PlayerModel>(true);
        forcefieldGo = Instantiate(Rsc.GoEnemiesOrObjMap["Forcefield"], playerModel.transform);
    }



    protected override void OnEndPlayerTurn(PlayerScript player, List<AbilityQueueItem> abilityQueue)
    {
        if(Settings.UseSimultaniousTurns) { return; }

        if(playerScript == player)
        {
            // beurt van activatie + 1 andere beurt actief!
            if(GameHandler.instance.CurrentTurn >= TurnActivated + 1)
            {
                DestroyForcefield();
            }
        }
    }

    protected override void OnAllPlayersFinishedTurn()
    {
        if (!Settings.UseSimultaniousTurns) { return; }

        if (GameHandler.instance.CurrentTurn >= TurnActivated + 1) // direct weg
        {
            DestroyForcefield();
        }
    }

    public int TakeDamage(int damage)
    {
        if (CurrentHitPoints < damage)
        {
            var damageLeft = damage - CurrentHitPoints;
            DestroyForcefield();
            return damageLeft;
        }
        else if (CurrentHitPoints == damage)
        {
            DestroyForcefield();
            return 0;
        }

        CurrentHitPoints = CurrentHitPoints - damage;
        return 0;
    }

    private void DestroyForcefield()
    {
        Destroy(forcefieldGo);
        Destroy(this);
    }
}
