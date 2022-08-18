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
            ShowIntroDialog();
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

Actions:
- Radar: Show an approximation of the player closest to you (up to 7 tiles shown; 1 of them has the player)
- Vision: Choose a tile to remove the fog
- Binoculars: +1 Vision till the end of your next turn
- Beartrap: Place a trap that is hidden to others :) If they step on it, they lose 1 HP. Beartrap vanishes after 2 turns. 1 trap placed per turn
- Rocket: Fires a rocket....duh. 1 damage if you hit a player. 1 rocket per turn.
- Moving: Move or.....If there is already someone there.... attack him/her! Be carefull, they might strike back if they don't die immediately.

Tile information:
- Green tile: Nothing happens
- Purple darkess tile: Lose 1 vision till the end of next turn
- Lava tile: Lose 1 HP
- Desert tile: Lose 2 Action points
- Stone path tile: Gain 1 Action point
- Well on tile: Gain 1 HP


Everyone starts in the corner - good luck!


PS Is everyone dead besides you? You win!
            "
            , buttons);
    }
}
