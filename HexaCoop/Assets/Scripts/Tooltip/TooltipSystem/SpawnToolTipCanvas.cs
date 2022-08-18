using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnToolTipCanvas : MonoBehaviour
{
    public TooltipSystem ToolTipPrefab;

    private void Start()
    {
        Instantiate(ToolTipPrefab);
    }
}