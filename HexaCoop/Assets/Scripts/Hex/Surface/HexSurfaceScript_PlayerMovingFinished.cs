using UnityEngine;

public partial class HexSurfaceScript : HexaEventCallback
{
    protected override void OnPlayerMovingFinished(PlayerScript player, Hex hexMovedTo)
    {
        if (hex == hexMovedTo)
        {
            switch (hex.HexSurfaceType)
            {
                case HexSurfaceType.Desert_Sand:
                    player.GetComponent<PlayerActionPoints>().DecreaseAP(2);
                    Textt.GameLocal("Desert surface: Lose 2 Actionpoints", playerFilter: player);
                    break;
                case HexSurfaceType.Light_Grey_Stone:
                case HexSurfaceType.Big_Brown_Stones:
                    player.GetComponent<PlayerActionPoints>().IncreaseAP(1);
                    Textt.GameLocal("Stone surface: Gain 1 Actionpoint", playerFilter: player);
                    break;
                case HexSurfaceType.Sand_Dirt:
                    player.GetComponent<PlayerActionPoints>().DecreaseAP(2);
                    Textt.GameLocal("Dirt surface: Lose 2 Actionpoints", playerFilter: player);
                    break;               
                case HexSurfaceType.Magma:
                case HexSurfaceType.Lava_Stones:

                    // TODO Netter oplossen tzt
                    if (player.GetComponent<PlayerFireImmumeScript>() != null)
                    {
                        return;
                    }

                    player.GetComponent<PlayerHealth>().TakeDamage(1);
                    Textt.GameLocal("Magma surface: Lose 1 Health", playerFilter: player);
                    break;
                case HexSurfaceType.Ice:

                    player.GetComponent<PlayerActionPoints>().IncreaseAP(AbilityType.Movement.GetCost());
                    Textt.GameLocal("Ice surface: Move/Slide to a neighbour tile", playerFilter: player);

                    if (Netw.CurrPlayer().IsOnMyNetwork())
                    {
                        var neighbourtiles = HexGrid.instance.GetNeighboursFor(player.CurrentHexTile.HexCoordinates);
                        neighbourtiles.Shuffle();
                        player.Ability(neighbourtiles[0].GetHex(), AbilityType.Movement);
                    }

                    break;

            }
        }
    }
}
