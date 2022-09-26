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
            var go = Instantiate(to.GetStructurePrefabFromRrc(), structureGo.transform);
            go.transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }    
}
