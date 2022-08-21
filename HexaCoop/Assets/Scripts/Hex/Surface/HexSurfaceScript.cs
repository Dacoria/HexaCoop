using UnityEngine;

public partial class HexSurfaceScript : HexaEventCallback
{
    [ComponentInject] private Hex hex;

    public void HexSurfaceTypeChanged(HexSurfaceType to)
    {
        var mainGo = GetMainGo();
        if (mainGo == null || mainGo.transform.childCount == 0)
        {
            return;
        }

        var getGoSurfaceBase = mainGo.transform.GetChild(0);

        if (Rsc.MaterialTileMap.TryGetValue(to.ToString(), out Material result))
        {
            var meshRenderer = getGoSurfaceBase.GetComponent<MeshRenderer>();
            meshRenderer.material = result;
        }
    }

    private GameObject GetMainGo() => Utils.GetChildGoByName(hex.gameObject, "Main");
}
