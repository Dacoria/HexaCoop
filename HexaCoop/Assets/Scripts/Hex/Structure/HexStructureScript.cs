using UnityEngine;

public partial class HexStructureScript: HexaEventCallback
{
    [ComponentInject] private Hex hex;

    public void HexStructureTypeChanged(HexStructureType to)
    {
        var structureGo = Utils.GetChildGoByName(hex.gameObject, "Props");
        MonoHelper.instance.DestroyChildrenOfGo(structureGo);
        if (to.HasStructure())
        {
            if (Rsc.GoStructureMap.TryGetValue(to.ToString() + "Structure", out GameObject result))
            {
                var go = Instantiate(result, structureGo.transform);
                go.transform.rotation = new Quaternion(0, 180, 0, 0);
            }
        }
    }

    private void DestroyMountain(Hex hex)
    {
        var mountainGo = Utils.GetStructureGoFromHex(hex);
        var lerpScript = mountainGo.GetAdd<LerpMovement>();
        lerpScript.MoveDown(distance: 1.5f, duration: 2f, callbackOnFinished: () => hex.ChangeHexStructureType(HexStructureType.None));
    }
}
