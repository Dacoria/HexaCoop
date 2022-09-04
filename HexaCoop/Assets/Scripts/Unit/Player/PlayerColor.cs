using System.Collections.Generic;
using UnityEngine;

public class PlayerColor : HexaEventCallback
{
    public GameObject ObjectOfPlayerToColor;
    [ComponentInject] private PlayerScript player;

    private Color _color = Color.white;
    public Color Color
    {
        get => _color; 
        set {
            _color = value;

            var renderers = ObjectOfPlayerToColor.GetComponents<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                foreach(var mat in renderer.materials)
                {
                    mat.color = _color;
                }
            }    
        }
    }

    protected override void OnNewRoundStarted(List<PlayerScript> allPlayers, PlayerScript currentPlayer)
    {
        // Altijd via deze volgorde; vandaar kloppen de kleuren ook
        for (int i = 0; i < allPlayers.Count; i++)
        {
            if (allPlayers[i] == player)
            {
                Color = GetPlayerColorForIndex(i);
                return;
            }
        }
    }

    private Color GetPlayerColorForIndex(int i) => i switch
    {
        0 => Colorr.Orange,
        1 => Colorr.LightBlue,
        2 => Colorr.Purple,
        3 => Colorr.Yellow,
        _ => Colorr.White,
    };
}
