using System;
using System.Collections.Generic;
using UnityEngine;

public static class ActionEvents
{
    // network
    public static Action<PlayerScript, Hex, AbilityType, int> PlayerAbility;
    public static Action<PlayerScript, Hex, AbilityType, int> PlayerAbilityNotExecuted;


    public static Action<List<PlayerScript>, PlayerScript> NewRoundStarted;
    public static Action<PlayerScript> NewPlayerTurn;
    public static Action<PlayerScript, List<AbilityQueueItem>> EndPlayerTurn;
    public static Action<List<AbilityQueueItem>> StartAbilityQueue;
    public static Action AllPlayersFinishedTurn;
    public static Action<bool, PlayerScript> EndRound;
    public static Action EndGame;
    public static Action EnemyFaseStarted;
    public static Action<PlayerScript, Hex, DamageObjectType> PlayerDamageObjectHitTile;
    public static Action<PlayerScript, Hex, PlayerScript> PlayerBeartrapHitPlayer;
    public static Action<EnemyScript, Hex> EnemyMove;
    public static Action<EnemyScript, PlayerScript> EnemyAttack;
    public static Action<PlayerScript> PlayerDied;
    public static Action<List<PlayerScript>> NewSimTurnsPlayOrder;

    // local
    public static Action GridLoaded;

    public static Action<IUnit> UnitMovingFinished;
    public static Action<IUnit, Hex, int> UnitAttackHit;
    public static Action<PlayerScript, Hex> PlayerHasTeleported;

    public static Action<Animator> DieAnimationFinished;
    public static Action<GameObject> AttackAnimationFinished;

    public static Action<AbilityQueueItem> RemoveQueueItem;
    public static Action<PlayerScript, Hex, AbilityType> PlayerAbilityQueue;



    // afgeleiden
    public static Action<PlayerScript> PlayerMovingFinished;
    public static Action<EnemyScript> EnemyMovingFinished;
    public static Action<PlayerScript, Hex, int> PlayerAttackHit;
    public static Action<EnemyScript, Hex, int> EnemyAttackHit;
}