using System.Linq;
using UnityEngine;

public class BombScript : HexaEventCallback, IObjectOnTile
{
    public Hex hexBombIsOn; // private faalt bij Explode functie; geen idee waarom (null). Dit werkt wel
    public GameObject ShockwaveEffectPrefab;

    public Hex CurrentHexTile => hexBombIsOn;
    

    private int neighbourRange = 1;


    public void SetCurrentHexTile(Hex hex)
    {
        hexBombIsOn = hex;
    }

    protected override void OnAllPlayersFinishedTurn()
    {
        Explode();
    }

    private void Explode()
    {
        var tilesThatTakeDamage = HexGrid.instance.GetNeighboursFor(hexBombIsOn.HexCoordinates, range: neighbourRange, excludeObstacles: false);
        tilesThatTakeDamage.Add(hexBombIsOn.HexCoordinates);

        ActionEvents.BombExplosionHit?.Invoke(tilesThatTakeDamage.Select(x => x.GetHex()).ToList(), 1);
        Instantiate(ShockwaveEffectPrefab, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }
}
