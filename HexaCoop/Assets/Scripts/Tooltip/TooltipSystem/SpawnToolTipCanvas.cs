using UnityEngine;

public class SpawnToolTipCanvas : MonoBehaviour
{
    public TooltipSystem ToolTipPrefab;

    private void Start()
    {
        Instantiate(ToolTipPrefab);
    }
}