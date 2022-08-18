using System.Linq;
using UnityEngine;
using System.Collections;

public class EnemyAttack : HexaEventCallback
{
    [ComponentInject] private EnemyScript enemyScript;
    [ComponentInject] private Animator animator;

    private int AttackDamage = 1;
    public int GetAttackDamage() => AttackDamage;

    protected override void OnEnemyAttack(EnemyScript enemy, PlayerScript player)
    {
        if(enemy.Id == enemyScript.Id)
        {
            AttackPlayer(player);
        }
    }

    private PlayerScript playerToAttack;

    private void AttackPlayer(PlayerScript player)
    {
        playerToAttack = player;
        RotateMoveTowardsPlayer(player.CurrentHexTile.HexCoordinates);
    }

    private Vector3 attackTargetV3;

    private void RotateMoveTowardsPlayer(Vector3Int hexCoordinateToMoveTowards)
    {
        var rotateLerp = GetComponent<LerpRotation>() ?? gameObject.AddComponent<LerpRotation>();

        attackTargetV3 = hexCoordinateToMoveTowards.GetHex().transform.position + new Vector3(0, 1, 0);

        rotateLerp.RotateTowardsDestination(endPosition: attackTargetV3, callbackOnFinished: AttackPlayerMove);
    }

    private bool attackFinished = false;

    private void AttackPlayerMove()
    {
        attackFinished = false;
        animator.SetTrigger(Statics.ANIMATION_TRIGGER_ATTACK);
        StartCoroutine(AttackFinished(1));// garantie?!
    }

    protected override void OnAttackAnimationFinished(GameObject animatorGo)
    {
        if(gameObject == animatorGo.transform.parent.gameObject)
        {
            StartCoroutine(AttackFinished(0.1f)); // wachttijd komt natuurlijker over
        }
    }

    private IEnumerator AttackFinished(float secondsToWait)
    {
        if(!attackFinished)
        {
            attackFinished = true;
            yield return new WaitForSeconds(secondsToWait);
            ActionEvents.EnemyAttackHit?.Invoke(enemyScript, playerToAttack.CurrentHexTile, AttackDamage);
            enemyScript.ActionFinished();
        }
        
    }
}
