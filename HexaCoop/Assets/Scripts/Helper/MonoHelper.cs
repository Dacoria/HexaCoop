using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MonoHelper : MonoBehaviour
{
    public static MonoHelper instance;

    private void Awake()
    {
        instance = this;
    }

    public AnimationCurve CurveGradual;

    public HighlightOneTileDisplayScript GetHighlightOneTileSelection(GameObject gameObject)
    {
        var highlightScripts = gameObject.GetComponents<HighlightOneTileDisplayScript>().ToList();
        if (!highlightScripts.Any())
        {
            return gameObject.AddComponent<HighlightOneTileDisplayScript>();
        }
        for (int i = 1; i < highlightScripts.Count(); i++)
        {
            Destroy(highlightScripts[i]);
        }

        highlightScripts[0].Reset();
        return highlightScripts[0];
    }

    public bool FindTile(Vector3 mousePosition, out List<Hex> result)
    {
        var layermask = 1 << LayerMask.NameToLayer(Statics.LAYER_MASK_HEXTILE);

        var ray = Camera.main.ScreenPointToRay(mousePosition);
        var hits = Physics.RaycastAll(ray, layermask);
        if (hits.Length > 0)
        {
            result = hits
                .Where(x => x.collider.gameObject.GetComponent<Hex>() != null)
                .Select(x => x.collider.gameObject.GetComponent<Hex>())
                .ToList();

            return result.Any();
        }

        result = null;
        return false;
    }

    public void DestroyGoAfterXSeconds(GameObject go, float seconds) => StartCoroutine(CR_DestroyGoAfterXSeconds(go, seconds));

    private IEnumerator CR_DestroyGoAfterXSeconds(GameObject go, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(go);
    }

    public int GenerateNewId()
    {
        var random = 0;
        while (random == 0)
        {
            random = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        }

        return random;
    }

    public void DestroyChildrenOfGo(GameObject go)
    {
        for (int i = go.transform.childCount - 1; i >= 0; i--)
        {
            var child = go.transform.GetChild(i);
            Destroy(child.gameObject);
        }
    }

    public Hex GetRandomTileAroundThisTile(Vector3Int tileToRandomize, int range = 1)
    {
        var gridsAroundOtherPlayer = HexGrid.instance.GetNeighboursFor(tileToRandomize, range: range);
        gridsAroundOtherPlayer.Add(tileToRandomize);

        // niet op de edge; dan krijg je niet 7 opties, maar minder -> zo min mogelijk edge
        gridsAroundOtherPlayer = gridsAroundOtherPlayer.Any(x => !x.IsOnEdgeOfGrid()) ?
            gridsAroundOtherPlayer.Where(x => !x.IsOnEdgeOfGrid()).ToList() :
            gridsAroundOtherPlayer.Where(x => !x.IsOnCornerOfGrid()).ToList();

        gridsAroundOtherPlayer.Shuffle();
        return gridsAroundOtherPlayer[0].GetHex();
    }


    public void SetHexLayoutBeforeGame(bool originalLayout)
    {
        var hexes = HexGrid.instance.GetAllTiles();
        hexes.ForEach(hex =>
        {
            hex.ChangeHexSurfaceType(originalLayout ? hex.HexSurfaceType : HexSurfaceType.Simple_Plain, alsoChangeType: false);
            hex.ChangeHexStructureType(originalLayout ? hex.HexStructureType : HexStructureType.None, alsoChangeType: false);
        });
        
        EnemyManager.instance.GetEnemies().ForEach(enemy => enemy.SetVisible(originalLayout));
    }
}