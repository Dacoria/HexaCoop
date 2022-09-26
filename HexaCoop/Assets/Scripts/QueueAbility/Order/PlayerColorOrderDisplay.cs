using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerColorOrderDisplay : MonoBehaviour
{
    [SerializeField] private Image imageCircle;
    [SerializeField] private Image imageVink;

    [HideInInspector] public PlayerScript player;

    public void SetColor(PlayerScript player)
    {
        this.player = player;
        imageCircle.color = player.Color;        
        imageVink.gameObject.SetActive(false);
    }

    public void SetReady()
    {
        imageVink.gameObject.SetActive(true);
    }
}
