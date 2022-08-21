using System;
using UnityEngine;

public partial class ExtendAE : MonoBehaviour
{
    private void Start()
    {
        ActionEvents.UnitAttackHit += OnUnitAttackHit;
        ActionEvents.UnitMovingFinished += OnUnitMovingFinished;
    }

    private void OnDestroy()
    {
        ActionEvents.UnitAttackHit -= OnUnitAttackHit;
        ActionEvents.UnitMovingFinished -= OnUnitMovingFinished;
    }

    // REST WORDT INGELADEN VIA PARTIAL CLASSES
}