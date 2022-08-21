using UnityEngine;

public class HexSurfaceScript : HexaEventCallback
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

    protected override void OnPlayerMovingFinished(PlayerScript player)
    {
        if (player.CurrentHexTile == hex)
        {
            switch (hex.HexSurfaceType)
            {
                case HexSurfaceType.Desert_Sand:
                    player.GetComponent<PlayerActionPoints>().DecreaseAP(2);
                    Textt.GameLocal("Desert surface: Lose 2 Actionpoints", playerFilter: player);
                    break;
                case HexSurfaceType.Light_Grey_Stone:
                case HexSurfaceType.Granite_Stone:
                case HexSurfaceType.Grey_Stone:
                case HexSurfaceType.Dirt_Stones:
                case HexSurfaceType.Big_Brown_Stones:
                    player.GetComponent<PlayerActionPoints>().IncreaseAP(1);
                    Textt.GameLocal("Stone surface: Gain 1 Actionpoint", playerFilter: player);
                    break;
                case HexSurfaceType.Sand_Dirt:
                    player.GetComponent<PlayerActionPoints>().DecreaseAP(2);
                    Textt.GameLocal("Dirt surface: Lose 2 Actionpoints", playerFilter: player);
                    break;
                case HexSurfaceType.Purple_Cracks:
                    var visionScript = player.gameObject.AddComponent<PlayerExtraVisionRangeScript>();
                    visionScript.AdditionalRange = -1;
                    Textt.GameLocal("Darkness surface: Lose 1 Vision till end of next turn", playerFilter: player);
                    break;
                case HexSurfaceType.Magma:
                case HexSurfaceType.Lave_Stones:
                    player.GetComponent<PlayerHealth>().TakeDamage(1);
                    Textt.GameLocal("Magma surface: Lose 1 Health", playerFilter: player);
                    break;
                case HexSurfaceType.Ice:

                    player.GetComponent<PlayerActionPoints>().IncreaseAP(AbilityType.Movement.Cost());
                    Textt.GameLocal("Ice surface: Move/Slide to a neighbour tile", playerFilter: player);

                    if (Netw.CurrPlayer().IsOnMyNetwork())
                    {
                        var neighbourtiles = HexGrid.instance.GetNeighboursFor(player.CurrentHexTile.HexCoordinates);
                        neighbourtiles.Shuffle();
                        NetworkAE.instance.PlayerAbility(player, neighbourtiles[0].GetHex(), AbilityType.Movement);
                    }

                    break;

            }
        }
    }
}
