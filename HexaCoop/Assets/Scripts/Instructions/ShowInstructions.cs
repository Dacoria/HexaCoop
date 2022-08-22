using Chibi.Free;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowInstructions : HexaEventCallback
{
    public bool ShowInstructionOnGridLoad = true;

    protected override void OnGridLoaded()
    {
        if (ShowInstructionOnGridLoad)
        {
            //ShowIntroDialog();
        }
    }

    public void OnButtonClick()
    {
        ShowIntroDialog();
    }

    private void ShowIntroDialog()
    {
        var dialog = FindObjectOfType<Dialog>();
        var ok = new Dialog.ActionButton("OK", () =>
        {
            //Debug.Log("click ok");
        },
        new Color(72f / 255, 173f / 255, 211f / 255), colorButtonText: Color.white);
        Dialog.ActionButton[] buttons = { ok };
        dialog.ShowDialog("Instructions",
            @"
Goal: Be the 1st person to visit the 2 Crystals to win the game

Move around with WASD + mouse scroll + middle mouse button.
Every round you get an extra 5 Action Points to do stuff (up till 10). 
            "
            , buttons);
    }
}
