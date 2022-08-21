using UnityEngine;

public partial class HexStructureScript: HexaEventCallback
{
    protected override void OnPlayerMovingFinished(PlayerScript player)
    {
        if(player.CurrentHexTile == hex)
        {
            switch(hex.HexStructureType)
            {
                case HexStructureType.Well:
                    hex.GetComponentInChildren<WellScript>().PlayerSteppedOnStructure(player);
                    break;
                case HexStructureType.Portal:
                    hex.GetComponentInChildren<PortalScript>().PlayerSteppedOnStructure(player);
                    break;
                case HexStructureType.Forest:
                    Textt.GameLocal("Forest: Lose 2 Actionpoints", playerFilter: player);
                    player.GetComponent<PlayerActionPoints>().DecreaseAP(2);
                    break;
                case HexStructureType.Hill:
                    Textt.GameLocal("Hill: Lose 1 Actionpoint", playerFilter: player);
                    player.GetComponent<PlayerActionPoints>().DecreaseAP(1);
                    break;
                case HexStructureType.Crystal:

                    // text update gaat via crystals reached script
                    var crystalReachedScript = player.gameObject.GetSet<PlayerCrystalsReached>();
                    crystalReachedScript.ReachedCrystal(hex);
                    break;

            }
        }
    }
}
