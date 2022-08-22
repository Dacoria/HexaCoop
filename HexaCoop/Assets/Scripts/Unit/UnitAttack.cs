using System;
using System.Collections;
using UnityEngine;

public class UnitAttack : HexaEventCallback
{
    [ComponentInject] private Animator animator;
    [ComponentInject] private IUnit unit;

    private Hex hexAttackTarget;

    public void AttackUnitOnHex(Hex hex)
    {
        Debug.Log("AttackUnitOnHex");
        hexAttackTarget = hex;
        RotateTowardsDestination(endPosition: hex.transform.position);
    }

    private void RotateTowardsDestination(Vector3 endPosition)
    {
        Debug.Log("RotateTowardsDestination");
        var targetDirection = endPosition - transform.position;
        var lerpRotation = gameObject.GetSet<LerpRotation>();
        lerpRotation.RotateTowardsDestination(endPosition, callbackOnFinished: StartAttack);
    }

    private void StartAttack()
    {
        Debug.Log("StartAttack");
        animator.SetTrigger(Statics.ANIMATION_TRIGGER_ATTACK);
    }

    protected override void OnAttackAnimationFinished(GameObject animatorGo)
    {
        Debug.Log("OnAttackAnimationFinished");
        if (gameObject == animatorGo.transform.parent.gameObject)
        {
            ActionEvents.UnitAttackHit?.Invoke(unit, hexAttackTarget, 1);

            Debug.Log("OnAttackAnimationFinished2");

            if (hexAttackTarget.GetUnit()?.IsAlive == true)
            {
                ActionEvents.UnitAttackHit?.Invoke(hexAttackTarget.GetUnit(), unit.CurrentHexTile, 1);
                Debug.Log("OnAttackAnimationFinished3");
            }
        }
    }
}