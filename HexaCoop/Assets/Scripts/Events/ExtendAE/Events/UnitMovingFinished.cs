using UnityEngine;

public partial class ExtendAE : MonoBehaviour
{    
    private void OnUnitMovingFinished(IUnit unit)
    {
        switch(unit.UnitType)
        {
            case UnitType.Player:
                ActionEvents.PlayerMovingFinished?.Invoke((PlayerScript)unit);
                break;
            case UnitType.Enemy:
                ActionEvents.EnemyMovingFinished?.Invoke((EnemyScript)unit);
                break;
            default:
                throw new System.Exception();                    
        }
    }
}