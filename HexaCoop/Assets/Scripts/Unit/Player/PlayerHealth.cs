using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : HexaEventCallback
{
    [ComponentInject] private PlayerScript playerScript;

    public int CurrentHitPoints { get; private set; }
    public int InitHitPoints = 20;

    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript currentPlayer)
    {
        CurrentHitPoints = InitHitPoints;        
    }

    protected override void OnPlayerDamageObjectHitTile(PlayerScript playerOwner, Hex hex, DamageObjectType doType)
    {
        var playerOnTile = hex.GetPlayer();
        if (playerOnTile?.Id == playerScript.Id)
        {
            // TODO Netter oplossen tzt
            if(doType == DamageObjectType.MeteorStrike && playerScript.GetComponent<PlayerFireImmumeScript>() != null)
            {
                return;
            }

            TakeDamage(1);
        }
    }

    protected override void OnPlayerBeartrapHitPlayer(PlayerScript pOwnsTrap, Hex hex, PlayerScript pHit)
    {
        if (pHit.Id == playerScript.Id)
        {
            TakeDamage(1);
        }
    }

    protected override void OnPlayerAttackHit(PlayerScript player, Hex hexWithTargetHit, int damage)
    {
        if (!player != playerScript && hexWithTargetHit.GetPlayer()?.Id == playerScript.Id)
        {
            TakeDamage(damage);
        }
    }

    protected override void OnEnemyAttackHit(EnemyScript enemy, Hex hex, int damage)
    {       
        if (hex.GetPlayer()?.Id == playerScript.Id)
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
            if(Netw.CurrPlayer() == playerScript)
            {
                NetworkAE.instance.EndPlayerTurn(playerScript);
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
        playerScript.PlayerModel.gameObject.SetActive(false);
    }
}
