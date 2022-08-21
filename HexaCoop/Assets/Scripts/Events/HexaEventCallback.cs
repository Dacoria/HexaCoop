using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class HexaEventCallback : MonoBehaviour
{
    protected void Awake()
    {
        this.ComponentInject();
    }
    protected void OnEnable()
    {
        ActionEvents.GridLoaded += OnGridLoaded;
        ActionEvents.NewRoundStarted += OnNewRoundStarted;
        ActionEvents.NewPlayerTurn += OnNewPlayerTurn;
        ActionEvents.AllPlayersFinishedTurn += OnAllPlayersFinishedTurn;
        ActionEvents.EndPlayerTurn += OnEndPlayerTurn;
        ActionEvents.EndRound += OnEndRound;
        ActionEvents.EndGame += OnEndGame;
        ActionEvents.PlayerAbility += OnPlayerAbility;
        ActionEvents.UnitMovingFinished += OnUnitMovingFinished;
        ActionEvents.PlayerDamageObjectHitTile += OnPlayerDamageObjectHitTile;
        ActionEvents.PlayerBeartrapHitPlayer += OnPlayerBeartrapHitPlayer;
        ActionEvents.UnitAttackHit += OnUnitAttackHit;
        ActionEvents.EnemyFaseStarted += OnEnemyFaseStarted;
        ActionEvents.EnemyMove += OnEnemyMove;
        ActionEvents.EnemyAttack += OnEnemyAttack;
        ActionEvents.DieAnimationFinished += OnDieAnimationFinished;
        ActionEvents.AttackAnimationFinished += OnAttackAnimationFinished;
        ActionEvents.PlayerScriptHasTeleported += OnPlayerScriptHasTeleported;

        ActionEvents.EnemyAttackHit += OnEnemyAttackHit;
        ActionEvents.PlayerAttackHit += OnPlayerAttackHit;
        ActionEvents.EnemyMovingFinished += OnEnemyMovingFinished;
        ActionEvents.PlayerMovingFinished += OnPlayerMovingFinished;
    }

    protected void OnDisable()
    {
        ActionEvents.GridLoaded -= OnGridLoaded;
        ActionEvents.NewRoundStarted -= OnNewRoundStarted;
        ActionEvents.NewPlayerTurn -= OnNewPlayerTurn;
        ActionEvents.AllPlayersFinishedTurn -= OnAllPlayersFinishedTurn;
        ActionEvents.EndPlayerTurn -= OnEndPlayerTurn;
        ActionEvents.EndRound -= OnEndRound;
        ActionEvents.EndGame -= OnEndGame;
        ActionEvents.PlayerAbility -= OnPlayerAbility;
        ActionEvents.UnitMovingFinished -= OnUnitMovingFinished;
        ActionEvents.PlayerDamageObjectHitTile -= OnPlayerDamageObjectHitTile;
        ActionEvents.PlayerBeartrapHitPlayer -= OnPlayerBeartrapHitPlayer;
        ActionEvents.UnitAttackHit -= OnUnitAttackHit;
        ActionEvents.EnemyFaseStarted -= OnEnemyFaseStarted;
        ActionEvents.EnemyMove -= OnEnemyMove;
        ActionEvents.EnemyAttack -= OnEnemyAttack;
        ActionEvents.DieAnimationFinished -= OnDieAnimationFinished;
        ActionEvents.AttackAnimationFinished -= OnAttackAnimationFinished;
        ActionEvents.PlayerScriptHasTeleported -= OnPlayerScriptHasTeleported;

        ActionEvents.EnemyAttackHit -= OnEnemyAttackHit;
        ActionEvents.PlayerAttackHit -= OnPlayerAttackHit;
        ActionEvents.EnemyMovingFinished -= OnEnemyMovingFinished;
        ActionEvents.PlayerMovingFinished -= OnPlayerMovingFinished;
    }

    protected virtual void OnGridLoaded() { }    
    protected virtual void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript player) { }
    protected virtual void OnNewPlayerTurn(PlayerScript player) { }
    protected virtual void OnAllPlayersFinishedTurn() { }
    protected virtual void OnEndRound(bool reachedMiddle, PlayerScript pWinner) { }
    protected virtual void OnEndGame() { }
    protected virtual void OnEndPlayerTurn(PlayerScript player) { }
    protected virtual void OnPlayerAbility(PlayerScript player, Hex hex, AbilityType abilityType) { }
    protected virtual void OnUnitMovingFinished(IUnit unit) { }
    protected virtual void OnPlayerDamageObjectHitTile(PlayerScript player, Hex hex, DamageObjectType doType) { }
    protected virtual void OnPlayerBeartrapHitPlayer(PlayerScript playerOwnsTrap, Hex hex, PlayerScript playerHit) { }
    protected virtual void OnUnitAttackHit(IUnit player, Hex hexWithTargetHit, int damage) { }
    protected virtual void OnEnemyFaseStarted() { }
    protected virtual void OnEnemyMove(EnemyScript enemy, Hex tile) { }
    protected virtual void OnEnemyAttack(EnemyScript enemy, PlayerScript player) { }
    protected virtual void OnEnemyAttackHit(EnemyScript enemy, Hex hex, int damage) { }
    protected virtual void OnDieAnimationFinished(Animator animator) { }
    protected virtual void OnAttackAnimationFinished(GameObject animatorGo) { }
    protected virtual void OnPlayerScriptHasTeleported(PlayerScript player, Hex hex) { }
    protected virtual void OnPlayerMovingFinished(PlayerScript player) { }
    protected virtual void OnEnemyMovingFinished(EnemyScript enemy) { }
    protected virtual void OnPlayerAttackHit(PlayerScript player, Hex hex, int damage) { }
}