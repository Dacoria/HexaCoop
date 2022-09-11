using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelLoaderButtonScript : MonoBehaviour, IPointerDownHandler
{
    private ConnectToServer connToServer;

    [ComponentInject] private Image image;
    public TMP_InputField nameInput;

    private Color colorNormal;
    private Color colorFaded; // knop ingedrukt

    private void Awake()
    {
        this.ComponentInject();
        colorNormal = image.color;
        colorFaded = image.color.SetAlpha(0.5f);
    }

    void Start()
    {
        connToServer = GameObject.FindObjectOfType<ConnectToServer>();
    }

    void Update()
    {
        isInteractable = !connToServer.HasStartedGame && (nameInput.text != null && nameInput.text.Length > 1);
        image.color = isInteractable ? colorNormal : colorFaded;
    }

    private bool isInteractable;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isInteractable)
        {
            connToServer.StartGameOnClick(gameObject.name);
        }
    }
}
