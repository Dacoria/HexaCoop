using UnityEngine;

public class UnitAttack : HexaEventCallback
{
    [ComponentInject] private Animator animator;
    [ComponentInject] private IUnit unit;

    private Hex hexAttackTarget;

    public void AttackUnitOnHex(Hex hex)
    {
        hexAttackTarget = hex;
        RotateTowardsDestination(endPosition: hex.transform.position);
    }

    private void RotateTowardsDestination(Vector3 endPosition)
    {
        var targetDirection = endPosition - transform.position;
        var lerpRotation = gameObject.GetSet<LerpRotation>();
        lerpRotation.RotateTowardsDestination(endPosition, callbackOnFinished: StartAttack);
    }

    private void StartAttack()
    {
        animator.SetTrigger(Statics.ANIMATION_TRIGGER_ATTACK);
    }

    protected override void OnAttackAnimationFinished(GameObject animatorGo)
    {
        if (gameObject == animatorGo.transform.parent.gameObject)
        {
            ActionEvents.UnitAttackHit?.Invoke(unit, hexAttackTarget, 1);

            if (hexAttackTarget.GetUnit()?.IsAlive == true)
            {
                ActionEvents.UnitAttackHit?.Invoke(hexAttackTarget.GetUnit(), unit.CurrentHexTile, 1);
            }
        }
    }
}