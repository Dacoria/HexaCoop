using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HexaEventCallback
{
    [ComponentInject] private PlayerScript player;

    public int CurrentHitPoints { get; private set; }
    public int InitHitPoints = 20;

    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript currentPlayer)
    {
        CurrentHitPoints = InitHitPoints;        
    }

    protected override void OnPlayerDamageObjectHitTile(PlayerScript playerOwner, Hex hex, DamageObjectType doType)
    {
        var playerOnTile = hex.GetPlayer();
        if (playerOnTile?.Id == player.Id)
        {
            // TODO Netter oplossen tzt
            if(doType == DamageObjectType.MeteorStrike && player.GetComponent<PlayerFireImmumeScript>() != null)
            {
                return;
            }

            TakeDamage(1);
        }
    }

    protected override void OnPlayerBeartrapHitPlayer(PlayerScript pOwnsTrap, Hex hex, PlayerScript pHit)
    {
        if (pHit.Id == player.Id)
        {
            TakeDamage(1);
        }
    }

    protected override void OnPlayerAttackHit(PlayerScript player, Hex hexWithTargetHit, int damage)
    {
        if (!player != this.player && hexWithTargetHit.GetPlayer()?.Id == this.player.Id)
        {
            TakeDamage(damage);
        }
    }

    protected override void OnEnemyAttackHit(EnemyScript enemy, Hex hex, int damage)
    {       
        if (hex.GetPlayer()?.Id == player.Id)
        {
            TakeDamage(damage);
        }
    }

    public void IncreaseHp(int hpIncrease) => CurrentHitPoints += hpIncrease;

    public void TakeDamage(int damage)
    {
        var forcefieldScript = GetComponent<PlayerForcefieldScript>();
        if(forcefieldScript != null)
        {
            var damageLeft = forcefieldScript.TakeDamage(damage);
            damage = damageLeft;
        }


        CurrentHitPoints = (int)Mathf.Max(CurrentHitPoints - damage, 0);
        if(CurrentHitPoints == 0)
        {
            Die();
            if(Netw.CurrPlayer() == player)
            {
                Netw.CurrPlayer().EndTurn();
            }
        }
    }

    private void Die()
    {
        StartCoroutine(StartDyingInXSeconds(0.5f));
    }

    private IEnumerator StartDyingInXSeconds(float seconds)
    {
        yield return Wait4Seconds.Get(seconds);
        GetComponentInChildren<Animator>(true).SetTrigger(Statics.ANIMATION_TRIGGER_DIE);

        if (PhotonNetwork.IsMasterClient)
        {
            // dit proces moet 1 iemand instantieren --> voor nu: masterclient (want is er altijd 1 van)
            GameHandler.instance.CheckEndOfRound();
        }
    }

    protected override void OnDieAnimationFinished(Animator animator)
    {
        if (animator.transform.parent.gameObject == gameObject)
        {
            StartCoroutine(HidePlayerModelInXSeconds(1f));
        }
    }

    private IEnumerator HidePlayerModelInXSeconds(float seconds)
    {
        yield return Wait4Seconds.Get(seconds);
        player.PlayerModel.gameObject.SetActive(false);
    }
}
