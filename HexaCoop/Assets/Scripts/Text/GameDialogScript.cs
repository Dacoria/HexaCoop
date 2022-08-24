using UnityEngine;
using TMPro;
using System.Linq;

public class GameDialogScript : MonoBehaviour
{
    public TMP_Text TextDialogLastLine;
    public TMP_Text TextDialogPreviousLines;
    public static GameDialogScript instance;

    private void Awake()
    {
        this.ComponentInject();
        instance = this;
        TextDialogLastLine.text = "";
        TextDialogPreviousLines.text = "";
    }

    public void AddText(string additionalText)
    {
        var allPreviousLines = TextDialogPreviousLines.text.Split("\n").Where(x => x != "");
        allPreviousLines = new[] { TextDialogLastLine.text }.Concat(allPreviousLines).Take(10).ToArray();
        TextDialogPreviousLines.text = string.Join("\n", allPreviousLines);

        var newLine = "* " + additionalText;
        TextDialogLastLine.text = newLine;
    }
}