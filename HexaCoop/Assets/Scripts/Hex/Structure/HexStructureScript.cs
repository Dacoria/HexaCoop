using UnityEngine;

public class HexStructureScript: HexaEventCallback
{
    [ComponentInject] private Hex hex;

    public void HexStructureTypeChanged(HexStructureType to)
    {
        var structureGo = GetStructuresGo(hex);
        MonoHelper.instance.DestroyChildrenOfGo(structureGo);
        if (HasHexTypeStructures(to))
        {
            if (Rsc.GoStructureMap.TryGetValue(to.ToString() + "Structure", out GameObject result))
            {
                var go = Instantiate(result, structureGo.transform);
                go.transform.rotation = new Quaternion(0, 180, 0, 0);
            }
        }
    }    

    private bool HasHexTypeStructures(HexStructureType type)
    {
        switch (type)
        {
            case HexStructureType.Castle:
            case HexStructureType.Well:
            case HexStructureType.Hill:
            case HexStructureType.Market:
            case HexStructureType.Forest:
            case HexStructureType.Mountain:
            case HexStructureType.Crystal:
                return true;
            default:
                return false;
        }
    }

    private GameObject GetStructuresGo(Hex hex) => Utils.GetChildGoByName(hex.gameObject, "Props");

    protected override void OnMovingFinished(PlayerScript player)
    {
        if(player.CurrentHexTile == hex)
        {
            switch(hex.HexStructureType)
            {
                case HexStructureType.Well:
                    player.GetComponent<PlayerHealth>().CurrentHitPoints += 1;
                    Textt.GameLocal("Well: Gain 1 HP", playerFilter: player);
                    break;
                case HexStructureType.Forest:
                    Textt.GameLocal("Forest: Lose 2 Actionpoints", playerFilter: player);
                    player.GetComponent<PlayerActionPoints>().CurrentPlayerActionPoints -= 2;
                    break;
                case HexStructureType.Hill:
                    Textt.GameLocal("Hill: Lose 1 Actionpoint", playerFilter: player);
                    player.GetComponent<PlayerActionPoints>().CurrentPlayerActionPoints -= 2;
                    break;
                case HexStructureType.Crystal:

                    // text update gaat via crystals reached script
                    var crystalReachedScript = player.GetComponent<PlayerCrystalsReached>() ?? player.gameObject.AddComponent<PlayerCrystalsReached>();
                    crystalReachedScript.ReachedCrystal();
                    break;

            }
        }
    }
}
