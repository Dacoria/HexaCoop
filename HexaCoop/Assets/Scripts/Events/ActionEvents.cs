using System;
using System.Collections.Generic;
using UnityEngine;

public static class ActionEvents
{
    // network
    public static Action<PlayerScript, Hex, Hex, AbilityType, int> PlayerAbility;
    public static Action<PlayerScript, Hex, Hex, AbilityType, int> PlayerAbilityNotExecuted;


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
    public static Action<IUnit, Hex> UnitMovingFinished;


    // afgeleiden
    public static Action<PlayerScript, Hex> PlayerMovingFinished;
    public static Action<EnemyScript, Hex> EnemyMovingFinished;

    // local
    public static Action GridLoaded;

    public static Action<List<Hex>, int> BombExplosionHit;

    public static Action<IUnit, Hex, int> UnitAttackHit;
    public static Action<PlayerScript, Hex> PlayerHasTeleported;

    public static Action<Animator> DieAnimationFinished;
    public static Action<GameObject> AttackAnimationFinished;

    public static Action<List<AbilityQueueItem>> RemoveQueueItems;
    public static Action<PlayerScript, Hex, Hex, AbilityType> PlayerAbilityQueue;


    public static Action<PlayerScript, Hex, AbilityType> AbilityPickedUp;


    public static Action<PlayerScript, Hex, int> PlayerAttackHit;
    public static Action<EnemyScript, Hex, int> EnemyAttackHit;
}