using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerHealth : HexaEventCallback
{
    [ComponentInject] private PlayerScript player;

    public int CurrentHitPoints { get; private set; }
    private int InitHitPoints => NetworkHelper.instance.GetAllPlayers().Count > 2 ? 1 : 2;

    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript currentPlayer)
    {
        CurrentHitPoints = InitHitPoints;        
    }

    protected override void OnPlayerDamageObjectHitTile(PlayerScript playerOwner, Hex hex, DamageObjectType doType)
    {
        var playerOnTile = hex.GetPlayer(isAlive: true);
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
        if (!player != this.player && hexWithTargetHit.GetPlayer(isAlive: true)?.Id == this.player.Id)
        {
            TakeDamage(damage);
        }
    }

    protected override void OnEnemyAttackHit(EnemyScript enemy, Hex hex, int damage)
    {       
        if (hex.GetPlayer(isAlive: true)?.Id == player.Id)
        {
            TakeDamage(damage);
        }
    }

    protected override void OnBombExplosionHit(List<Hex> tiles, int damage)
    {
        if(tiles.Any(tile => tile == player.CurrentHexTile))
        {
            TakeDamage(damage);
        }
    }

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
        }
    }

    private void Die()
    {
        if(!PhotonNetwork.IsMasterClient) { return; }
        NetworkAE.instance.PlayerDied(player);
    }

    protected override void OnPlayerDied(PlayerScript playerThatDied)
    {
        if (playerThatDied == player)
        { 
            CurrentHitPoints = 0;
            StartCoroutine(StartDyingInXSeconds(0.5f));
        }
    }

    private IEnumerator StartDyingInXSeconds(float seconds)
    {
        yield return Wait4Seconds.Get(seconds);
        GetComponentInChildren<Animator>(true).SetTrigger(Statics.ANIMATION_TRIGGER_DIE);
        StartCoroutine(HidePlayerModelInXSeconds(2f));
        if (PhotonNetwork.IsMasterClient)
        {
            GameHandler.instance.CheckEndOfRound();
        }
    }

    private IEnumerator HidePlayerModelInXSeconds(float seconds)
    {
        yield return Wait4Seconds.Get(seconds);
        player.PlayerModel.gameObject.SetActive(false);
    }
}