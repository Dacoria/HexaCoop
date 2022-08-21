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
Welcome to Hexa!

Goal: Be the 1st person to visit the 2 Crystals to win the game. Tip: Don't die on your way there.
Additional tip: Killing others will increase your chance of winning. You only have 2 HP, but so do the others!

Move around with WASD + mouse scroll + middle mouse button.
Every round you get an extra 5 Action Points to do stuff (up till 10). 

Some tiles have an effect when you step on them.

Everyone starts in the corner - good luck!


PS Is everyone dead besides you? You win!
            "
            , buttons);
    }
}
