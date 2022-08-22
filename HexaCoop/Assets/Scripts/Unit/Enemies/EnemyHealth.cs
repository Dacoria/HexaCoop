using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : HexaEventCallback
{
    [ComponentInject] private EnemyScript enemyScript;
    [ComponentInject] private Animator animator;

    public int CurrentHitPoints;
    public int InitHitPoints;

    private new void Awake()
    {
        base.Awake();
        InitHitPoints = 2;
    }

    private new void OnEnable()
    {
        base.OnEnable();
        CurrentHitPoints = InitHitPoints;
    }

    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript currentPlayer)
    {
        CurrentHitPoints = InitHitPoints;
    }

    protected override void OnPlayerDamageObjectHitTile(PlayerScript playerOwner, Hex hexWithTargetHit, DamageObjectType doType)
    {
        if (hexWithTargetHit == enemyScript.CurrentHexTile)
        {
            TakeDamage(1);
        }
    }

    protected override void OnPlayerAttackHit(PlayerScript player, Hex hexWithTargetHit, int damage)
    {
        if (hexWithTargetHit == enemyScript.CurrentHexTile)
        {
            TakeDamage(damage);
        }
    }

    private void TakeDamage(int damage)
    {
        CurrentHitPoints = (int)Mathf.Max(CurrentHitPoints - damage, 0);
        if(CurrentHitPoints == 0)
        {
            StartCoroutine(StartDyingInXSeconds(0.6f));
        }
    }   

    private IEnumerator StartDyingInXSeconds(float seconds)
    {
        yield return Wait4Seconds.Get(seconds);
        animator.SetTrigger(Statics.ANIMATION_TRIGGER_DIE);        
    }

    protected override void OnDieAnimationFinished(Animator animator)
    {
        if(animator.transform.parent.gameObject == gameObject)
        {
            Die();
        }        
    }

    private void Die()
    {
        gameObject.SetActive(false);

        if (PhotonNetwork.IsMasterClient)
        {
            // die proces moet 1 iemand instantieren --> voor nu: masterclient (want is er altijd 1 van)
            GameHandler.instance.CheckEndOfRound();
        }
    }
}
