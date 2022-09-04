using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerColorOrderDisplay : MonoBehaviour
{
    [SerializeField] private Image image;

    public void SetColor(Color color)
    {
        image.color = color;
    }
}
