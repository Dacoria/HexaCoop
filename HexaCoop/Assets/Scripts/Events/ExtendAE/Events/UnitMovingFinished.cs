using UnityEngine;

public partial class ExtendAE : MonoBehaviour
{    
    private void OnUnitMovingFinished(IUnit unit, Hex hex)
    {
        switch(unit.UnitType)
        {
            case UnitType.Player:
                ActionEvents.PlayerMovingFinished?.Invoke((PlayerScript)unit, hex);
                break;
            case UnitType.Enemy:
                ActionEvents.EnemyMovingFinished?.Invoke((EnemyScript)unit, hex);
                break;
            default:
                throw new System.Exception();                    
        }
    }
}