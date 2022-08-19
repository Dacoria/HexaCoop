using System.Collections;
using UnityEngine;

public class NetwHandleMovementAbility : HexaEventCallback, IAbilityNetworkHandler
{
    [ComponentInject] private PlayerMovement PlayerMovement;
    [ComponentInject] private Animator animator;

    private Hex newHexTile;
    private PlayerScript movingPlayer;

    public void NetworkHandle(PlayerScript playerDoingAbility, Hex target)
    {
        movingPlayer = playerDoingAbility;
        newHexTile = target;
        StartCoroutine(PlayerMovement.RotateTowardsDestination(newHexTile.transform.position, OnRotationFinished));
    }
        
    private void OnRotationFinished()
    {
        if (newHexTile.HasUnitOnHex())
        {
            AttackPlayerMove();
        }
        else
        {
            StartCoroutine(PlayerMovement.MoveToDestination(newHexTile.transform.position, duration: 1, callbackOnFinished: OnMovingFinished));
        }
    }

    private void AttackPlayerMove()
    {
        animator.SetTrigger(Statics.ANIMATION_TRIGGER_ATTACK);
    }

    protected override void OnAttackAnimationFinished(GameObject animatorGo)
    {
        if (gameObject == animatorGo.transform.parent.gameObject)
        {
            ActionEvents.PlayerAttackHit?.Invoke(movingPlayer, newHexTile, 1);

            // TODO: Moet nog beter -> instant retaliate (zonder animatie :S)
            if(newHexTile.GetPlayerOnHex()?.PlayerIsAlive == true)
            {
                ActionEvents.PlayerAttackHit?.Invoke(newHexTile.GetPlayerOnHex(), movingPlayer.CurrentHexTile, 1);
            }
        }
    }    

    private void OnMovingFinished()
    {
        movingPlayer.transform.position = newHexTile.transform.position;
        movingPlayer.CurrentHexTile = newHexTile;
        ActionEvents.MovingFinished?.Invoke(movingPlayer);
    }
}