using TMPro;
using UnityEngine;

public class PlayerTextScript : HexaEventCallback
{
    [ComponentInject] private PlayerScript playerScript;
    [ComponentInject] private TMP_Text playerNameText;     

    void Update()
    {
        playerNameText.text = playerScript.PlayerName;
        transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);

        playerNameText.color = Netw.CurrPlayer() == playerScript ? Color.red : Color.white;
    }
}