using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

public class HighlightOneTileDisplayScript : MonoBehaviour
{
    public Action<Hex> CallbackOnTileSelection;
    public Action<Hex> CallbackOnTileSelectionConfirmed;

    public bool DestroyOnCallbackSelection = false;
    public bool DestroyOnCallbackSelectionConfirmed = true;
    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            TryHandleTileSelection(mousePos);
        }
    }   

    private Hex HighlightedHex;

    private void TryHandleTileSelection(Vector3 mousePos)
    {
        List<Hex> selectedTiles;
        if (MonoHelper.instance.FindTile(mousePos, out selectedTiles))
        {
            if(HighlightedHex != null && selectedTiles.Any(x => x == HighlightedHex))
            {
                if(selectedTiles.Count == 1)
                {
                    TileSelectionConfirmed();
                }                
            }
            else if(!OnlyTileConfirmation)
            {
                TileSelection(selectedTiles);                
            }
        }
    }

    private void TileSelectionConfirmed()
    {
        HighlightedHex.DisableHighlight();
        CallbackOnTileSelectionConfirmed?.Invoke(HighlightedHex);
        if (DestroyOnCallbackSelectionConfirmed) { Destroy(this); }
    }


    // voor als je wegklikt, dat dan dezelfde tile geselecteerd staat (bv de hex vd player)
    private bool OnlyTileConfirmation = false;

    public void SetOnlyConfirmTileSelection(Hex hexTileToConfirm)
    {
        OnlyTileConfirmation = true;
        HighlightedHex = hexTileToConfirm;
        HighlightedHex.EnableHighlight(HighlightColorType.White);
    }

    private void TileSelection(List<Hex> result)
    {
        if (HighlightedHex != null)
        {
            HighlightedHex.DisableHighlight();
        }

        HighlightedHex = result.First();
        HighlightedHex.EnableHighlight(HighlightColorType.White);

        CallbackOnTileSelection?.Invoke(HighlightedHex);
        if(DestroyOnCallbackSelection) { Destroy(this); }
    }

    private void OnDestroy()
    {
        if(HighlightedHex != null)
        {
            HighlightedHex.DisableHighlight(HighlightColorType.White);
        }
    }

    public void Reset()
    {
        if (HighlightedHex != null)
        {
            HighlightedHex.DisableHighlight(HighlightColorType.White);
        }

        HighlightedHex = null;
        DestroyOnCallbackSelection = false;
        DestroyOnCallbackSelectionConfirmed = true;
        OnlyTileConfirmation = false;
    }
}
