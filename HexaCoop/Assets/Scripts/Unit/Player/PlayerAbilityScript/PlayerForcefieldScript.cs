using System.Collections.Generic;
using UnityEngine;

public class PlayerForcefieldScript : HexaEventCallback
{
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

    protected override void OnAllPlayersFinishedTurn()
    {
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
