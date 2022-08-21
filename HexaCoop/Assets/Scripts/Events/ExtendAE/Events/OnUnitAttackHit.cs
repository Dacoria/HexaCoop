using UnityEngine;

public partial class ExtendAE : MonoBehaviour
{
    private void OnUnitAttackHit(IUnit unit, Hex hex, int damage)
    {
        switch (unit.UnitType)
        {
            case UnitType.Player:
                ActionEvents.PlayerAttackHit?.Invoke((PlayerScript)unit, hex, damage);
                break;
            case UnitType.Enemy:
                ActionEvents.EnemyAttackHit?.Invoke((EnemyScript)unit, hex, damage);
                break;
            default:
                throw new System.Exception();
        }
    }
}