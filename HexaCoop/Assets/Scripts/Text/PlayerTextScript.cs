using TMPro;
using UnityEngine;

public class PlayerTextScript : HexaEventCallback
{
    [ComponentInject] private PlayerScript playerScript;
    [ComponentInject] private TMP_Text playerName;     

    void Update()
    {
        playerName.text = playerScript.PlayerName;
        transform.rotation = Quaternion.Euler(0, Camera.main.transform.eulerAngles.y, 0);
    }
}