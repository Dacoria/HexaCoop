using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public partial class GameHandler : HexaEventCallback
{
    private List<EnemyScript> EnemiesWithAction;

    private void DoEnemyFase(float secondsToWait)
    {
        NetworkAE.instance.EnemyFaseStarted();
        EnemiesWithAction = EnemyManager.instance.GetEnemies();
        TryDoEnemyAction();
    }

    private void TryDoEnemyAction()
    {
        if (EnemiesWithAction.Any())
        {
            var enemyAction = EnemiesWithAction.First();
            EnemiesWithAction.Remove(enemyAction);
            enemyAction.DoAction(EnemyActionFinished);
        }
        else
        {
            EnemyFaseFinished();
        }
    }

    private void EnemyActionFinished()
    {
        TryDoEnemyAction();
    }

    private void EnemyFaseFinished()
    {
        if (GameStatus != GameStatus.PlayerFase)
        {
            return;
        }

        throw new Exception("Parrallel vs Seq");
        // NextPlayerTurn();
    }
}
