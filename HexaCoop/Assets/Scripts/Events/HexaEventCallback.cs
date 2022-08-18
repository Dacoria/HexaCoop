﻿using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class HexaEventCallback : MonoBehaviour
{
    protected void Awake()
    {
        this.ComponentInject();
    }
    protected void Start()
    {
        ActionEvents.GridLoaded += OnGridLoaded;
        ActionEvents.NewRoundStarted += OnNewRoundStarted;
        ActionEvents.NewPlayerTurn += OnNewPlayerTurn;
        ActionEvents.AllPlayersFinishedTurn += OnAllPlayersFinishedTurn;
        ActionEvents.EndPlayerTurn += OnEndPlayerTurn;
        ActionEvents.EndRound += OnEndRound;
        ActionEvents.EndGame += OnEndGame;
        ActionEvents.PlayerAbility += OnPlayerAbility;
        ActionEvents.MovingFinished += OnMovingFinished;
        ActionEvents.PlayerRocketHitTile += OnPlayerRocketHitTile;
        ActionEvents.PlayerBeartrapHitPlayer += OnPlayerBeartrapHitPlayer;
        ActionEvents.PlayerAttackHit += OnPlayerAttackHit;
        ActionEvents.EnemyFaseStarted += OnEnemyFaseStarted;
        ActionEvents.EnemyMove += OnEnemyMove;
        ActionEvents.EnemyAttack += OnEnemyAttack;
        ActionEvents.EnemyAttackHit += OnEnemyAttackHit;
        ActionEvents.DieAnimationFinished += OnDieAnimationFinished;
        ActionEvents.AttackAnimationFinished += OnAttackAnimationFinished;
    }


    protected void OnDestroy()
    {
        ActionEvents.GridLoaded -= OnGridLoaded;
        ActionEvents.NewRoundStarted -= OnNewRoundStarted;
        ActionEvents.NewPlayerTurn -= OnNewPlayerTurn;
        ActionEvents.AllPlayersFinishedTurn -= OnAllPlayersFinishedTurn;
        ActionEvents.EndPlayerTurn -= OnEndPlayerTurn;
        ActionEvents.EndRound -= OnEndRound;
        ActionEvents.EndGame -= OnEndGame;
        ActionEvents.PlayerAbility -= OnPlayerAbility;
        ActionEvents.MovingFinished -= OnMovingFinished;
        ActionEvents.PlayerRocketHitTile -= OnPlayerRocketHitTile;
        ActionEvents.PlayerBeartrapHitPlayer -= OnPlayerBeartrapHitPlayer;
        ActionEvents.PlayerAttackHit -= OnPlayerAttackHit;
        ActionEvents.EnemyFaseStarted -= OnEnemyFaseStarted;
        ActionEvents.EnemyMove -= OnEnemyMove;
        ActionEvents.EnemyAttack -= OnEnemyAttack;
        ActionEvents.EnemyAttackHit -= OnEnemyAttackHit;
        ActionEvents.DieAnimationFinished -= OnDieAnimationFinished;
        ActionEvents.AttackAnimationFinished -= OnAttackAnimationFinished;
    }
    protected virtual void OnGridLoaded() { }    
    protected virtual void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript player) { }
    protected virtual void OnNewPlayerTurn(PlayerScript player) { }
    protected virtual void OnAllPlayersFinishedTurn() { }
    protected virtual void OnEndRound(bool reachedMiddle, PlayerScript pWinner) { }
    protected virtual void OnEndGame() { }
    protected virtual void OnEndPlayerTurn(PlayerScript player) { }
    protected virtual void OnPlayerAbility(PlayerScript player, Hex hex, AbilityType abilityType) { }
    protected virtual void OnMovingFinished(PlayerScript player) { }
    protected virtual void OnPlayerRocketHitTile(PlayerScript playerWhoShot, Hex hex) { }
    protected virtual void OnPlayerBeartrapHitPlayer(PlayerScript playerOwnsTrap, Hex hex, PlayerScript playerHit) { }
    protected virtual void OnPlayerAttackHit(PlayerScript player, Hex hexWithTargetHit, int damage) { }
    protected virtual void OnEnemyFaseStarted() { }
    protected virtual void OnEnemyMove(EnemyScript enemy, Hex tile) { }
    protected virtual void OnEnemyAttack(EnemyScript enemy, PlayerScript player) { }
    protected virtual void OnEnemyAttackHit(EnemyScript enemy, Hex hex, int damage) { }
    protected virtual void OnDieAnimationFinished(Animator animator) { }
    protected virtual void OnAttackAnimationFinished(GameObject animatorGo) { }
}