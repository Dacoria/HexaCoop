using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public abstract class HexaEventCallback : MonoBehaviour
{
    protected void Awake()
    {
        this.ComponentInject();
    }

    protected void OnEnable()
    {
        if (IsOverwritten("OnGridLoaded")) ActionEvents.GridLoaded += OnGridLoaded;
        if (IsOverwritten("OnNewRoundStarted")) ActionEvents.NewRoundStarted += OnNewRoundStarted;
        if (IsOverwritten("OnNewPlayerTurn")) ActionEvents.NewPlayerTurn += OnNewPlayerTurn;
        if (IsOverwritten("OnAllPlayersFinishedTurn")) ActionEvents.AllPlayersFinishedTurn += OnAllPlayersFinishedTurn;
        if (IsOverwritten("OnEndPlayerTurn")) ActionEvents.EndPlayerTurn += OnEndPlayerTurn;
        if (IsOverwritten("OnEndRound")) ActionEvents.EndRound += OnEndRound;
        if (IsOverwritten("OnEndGame")) ActionEvents.EndGame += OnEndGame;
        if (IsOverwritten("OnPlayerAbility")) ActionEvents.PlayerAbility += OnPlayerAbility;
        if (IsOverwritten("OnPlayerAbilityQueue")) ActionEvents.PlayerAbilityQueue += OnPlayerAbilityQueue;
        if (IsOverwritten("OnUnitMovingFinished")) ActionEvents.UnitMovingFinished += OnUnitMovingFinished;
        if (IsOverwritten("OnPlayerDamageObjectHitTile")) ActionEvents.PlayerDamageObjectHitTile += OnPlayerDamageObjectHitTile;
        if (IsOverwritten("OnPlayerBeartrapHitPlayer")) ActionEvents.PlayerBeartrapHitPlayer += OnPlayerBeartrapHitPlayer;
        if (IsOverwritten("OnUnitAttackHit")) ActionEvents.UnitAttackHit += OnUnitAttackHit;
        if (IsOverwritten("OnEnemyFaseStarted")) ActionEvents.EnemyFaseStarted += OnEnemyFaseStarted;
        if (IsOverwritten("OnEnemyMove")) ActionEvents.EnemyMove += OnEnemyMove;
        if (IsOverwritten("OnEnemyAttack")) ActionEvents.EnemyAttack += OnEnemyAttack;
        if (IsOverwritten("OnDieAnimationFinished")) ActionEvents.DieAnimationFinished += OnDieAnimationFinished;
        if (IsOverwritten("OnAttackAnimationFinished")) ActionEvents.AttackAnimationFinished += OnAttackAnimationFinished;
        if (IsOverwritten("OnPlayerScriptHasTeleported")) ActionEvents.PlayerScriptHasTeleported += OnPlayerScriptHasTeleported;
        if (IsOverwritten("OnRemoveQueueItem")) ActionEvents.RemoveQueueItem += OnRemoveQueueItem;
        if (IsOverwritten("OnStartAbilityQueue")) ActionEvents.StartAbilityQueue += OnStartAbilityQueue;

        if (IsOverwritten("OnEnemyAttackHit")) ActionEvents.EnemyAttackHit += OnEnemyAttackHit;
        if (IsOverwritten("OnPlayerAttackHit")) ActionEvents.PlayerAttackHit += OnPlayerAttackHit;
        if (IsOverwritten("OnEnemyMovingFinished")) ActionEvents.EnemyMovingFinished += OnEnemyMovingFinished;
        if (IsOverwritten("OnPlayerMovingFinished")) ActionEvents.PlayerMovingFinished += OnPlayerMovingFinished;
    }

    protected void OnDisable()
    {
        if (IsOverwritten("OnGridLoaded")) ActionEvents.GridLoaded -= OnGridLoaded;
        if (IsOverwritten("OnNewRoundStarted")) ActionEvents.NewRoundStarted -= OnNewRoundStarted;
        if (IsOverwritten("OnNewPlayerTurn")) ActionEvents.NewPlayerTurn -= OnNewPlayerTurn;
        if (IsOverwritten("OnAllPlayersFinishedTurn")) ActionEvents.AllPlayersFinishedTurn -= OnAllPlayersFinishedTurn;
        if (IsOverwritten("OnEndPlayerTurn")) ActionEvents.EndPlayerTurn -= OnEndPlayerTurn;
        if (IsOverwritten("OnEndRound")) ActionEvents.EndRound -= OnEndRound;
        if (IsOverwritten("OnEndGame")) ActionEvents.EndGame -= OnEndGame;
        if (IsOverwritten("OnPlayerAbility")) ActionEvents.PlayerAbility -= OnPlayerAbility;
        if (IsOverwritten("OnPlayerAbilityQueue")) ActionEvents.PlayerAbilityQueue -= OnPlayerAbilityQueue;
        if (IsOverwritten("OnUnitMovingFinished")) ActionEvents.UnitMovingFinished -= OnUnitMovingFinished;
        if (IsOverwritten("OnPlayerDamageObjectHitTile")) ActionEvents.PlayerDamageObjectHitTile -= OnPlayerDamageObjectHitTile;
        if (IsOverwritten("OnPlayerBeartrapHitPlayer")) ActionEvents.PlayerBeartrapHitPlayer -= OnPlayerBeartrapHitPlayer;
        if (IsOverwritten("OnUnitAttackHit")) ActionEvents.UnitAttackHit -= OnUnitAttackHit;
        if (IsOverwritten("OnEnemyFaseStarted")) ActionEvents.EnemyFaseStarted -= OnEnemyFaseStarted;
        if (IsOverwritten("OnEnemyMove")) ActionEvents.EnemyMove -= OnEnemyMove;
        if (IsOverwritten("OnEnemyAttack")) ActionEvents.EnemyAttack -= OnEnemyAttack;
        if (IsOverwritten("OnDieAnimationFinished")) ActionEvents.DieAnimationFinished -= OnDieAnimationFinished;
        if (IsOverwritten("OnAttackAnimationFinished")) ActionEvents.AttackAnimationFinished -= OnAttackAnimationFinished;
        if (IsOverwritten("OnPlayerScriptHasTeleported")) ActionEvents.PlayerScriptHasTeleported -= OnPlayerScriptHasTeleported;
        if (IsOverwritten("OnRemoveQueueItem")) ActionEvents.RemoveQueueItem -= OnRemoveQueueItem;
        if (IsOverwritten("OnStartAbilityQueue")) ActionEvents.StartAbilityQueue -= OnStartAbilityQueue;

        if (IsOverwritten("OnEnemyAttackHit")) ActionEvents.EnemyAttackHit -= OnEnemyAttackHit;
        if (IsOverwritten("OnPlayerAttackHit")) ActionEvents.PlayerAttackHit -= OnPlayerAttackHit;
        if (IsOverwritten("OnEnemyMovingFinished")) ActionEvents.EnemyMovingFinished -= OnEnemyMovingFinished;
        if (IsOverwritten("OnPlayerMovingFinished")) ActionEvents.PlayerMovingFinished -= OnPlayerMovingFinished;
    }

    protected virtual void OnGridLoaded() { }    
    protected virtual void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript player) { }
    protected virtual void OnNewPlayerTurn(PlayerScript player) { }
    protected virtual void OnAllPlayersFinishedTurn() { }
    protected virtual void OnEndRound(bool reachedMiddle, PlayerScript pWinner) { }
    protected virtual void OnEndGame() { }
    protected virtual void OnEndPlayerTurn(PlayerScript player, List<AbilityQueueItem> abilityQueue) { }
    protected virtual void OnPlayerAbility(PlayerScript player, Hex hex, AbilityType abilityType) { }
    protected virtual void OnPlayerAbilityQueue(PlayerScript player, Hex hex, AbilityType abilityType) { }
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
    protected virtual void OnRemoveQueueItem(AbilityQueueItem queueItem) { }
    protected virtual void OnStartAbilityQueue(List<AbilityQueueItem> abilityQueueItems) { }




    private bool IsOverwritten(string functionName)
    {
        var type = GetType();
        var method = type.GetMember(functionName, BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.Instance);
        return method.Length > 0;
    }
}