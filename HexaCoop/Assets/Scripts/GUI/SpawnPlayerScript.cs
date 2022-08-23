using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SpawnPlayerScript : MonoBehaviour
{
    private List<Button> buttons;

    private void Awake()
    {
        this.buttons = GetComponentsInChildren<Button>().ToList();        
    }

    public void OnButtonClick(bool useAi)
    {
        SpawnPlayers.instance.SpawnDummyPlayer(useAi);
    }

    private bool hasMaxAmountOfPlayers;

    private void Update()
    {
        if (GameHandler.instance.GameStatus != GameStatus.NotStarted)
        {
            buttons.ForEach(button => button.interactable = false);
        }
        else if (!hasMaxAmountOfPlayers)
        {
            hasMaxAmountOfPlayers = NetworkHelper.instance.GetAllPlayers().Count() >= GameHandler.instance.GetStartTilesCount();
        }
        else
        {
            buttons.ForEach(button => button.interactable = false);
        }
    }
}
