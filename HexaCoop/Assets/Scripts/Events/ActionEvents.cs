using System;
using System.Collections.Generic;
using UnityEngine;

public static class ActionEvents
{
    // network
    public static Action<PlayerScript, Hex, AbilityType> PlayerAbility;
    public static Action<List<PlayerScript>, PlayerScript> NewRoundStarted;
    public static Action<PlayerScript> NewPlayerTurn;
    public static Action<PlayerScript> EndPlayerTurn;
    public static Action AllPlayersFinishedTurn;
    public static Action<bool, PlayerScript> EndRound;
    public static Action EndGame;
    public static Action EnemyFaseStarted;
    public static Action<PlayerScript, Hex> PlayerRocketHitTile;
    public static Action<PlayerScript, Hex, PlayerScript> PlayerBeartrapHitPlayer;
    public static Action<PlayerScript, Hex, int> PlayerAttackHit;
    public static Action<EnemyScript, Hex> EnemyMove;
    public static Action<EnemyScript, PlayerScript> EnemyAttack;

    // local
    public static Action GridLoaded;
    public static Action<PlayerScript> MovingFinished;
    public static Action<EnemyScript, Hex, int> EnemyAttackHit; // momenteel local direct na enemy move -> logisch?
    public static Action<Animator> DieAnimationFinished;
    public static Action<GameObject> AttackAnimationFinished;
}