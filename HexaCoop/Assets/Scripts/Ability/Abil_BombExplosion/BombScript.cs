using System.Linq;
using UnityEngine;

public class BombScript : HexaEventCallback, IObjectOnTile
{
    private Hex hexBombIsOn;
    public GameObject ShockwaveEffectPrefab;

    public Hex CurrentHexTile => hexBombIsOn;
    public void SetCurrentHexTile(Hex hex) => hexBombIsOn = hex;


    private int neighbourRange = 1;

    protected override void OnAllPlayersFinishedTurn()
    {
        Explode();
    }

    private void Explode()
    {
        var tilesThatTakeDamage = HexGrid.instance.GetNeighboursFor(
            hexBombIsOn.HexCoordinates, 
            range: neighbourRange, 
            excludeObstacles: false, 
            includeStartHex: true
        );

        ActionEvents.BombExplosionHit?.Invoke(tilesThatTakeDamage.Select(x => x.GetHex()).ToList(), 1);
        Instantiate(ShockwaveEffectPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
