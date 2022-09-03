using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

public class GameDialogScript : HexaEventCallback
{
    public TMP_Text TextDialogLines;
    public static GameDialogScript instance;

    public bool UseTypeWriterEffect = true;
    private float TypeWriterSpeed = 30;

    private new void Awake()
    {
        base.Awake();
        instance = this;
        Reset();
    }

    private string finalText = "";

    public void Reset()
    {
        TextDialogLines.text = "";
        finalText = "";
    }

    public void AddText(string additionalText)
    {
        var allPreviousLines = finalText.Split("\n").Where(x => x != "").ToList();

        var lastNumberAdded = 0;
        if(allPreviousLines.Any())
        {
            lastNumberAdded = int.Parse(allPreviousLines[0].Split(" ")[0].Replace(".", ""));
        }

        var newLine = (lastNumberAdded + 1) + ". " + additionalText;
        var newLinesArray = new[] { newLine }.Concat(allPreviousLines).ToArray();
        finalText = string.Join("\n", newLinesArray);

        if (UseTypeWriterEffect)
        {
            StartCoroutine(NewTextApearsInTypeWriterStyle(newLine, allPreviousLines));
        }
        else
        {
            TextDialogLines.text = finalText;
        }        
    }

    private IEnumerator NewTextApearsInTypeWriterStyle(string newLine, List<string> allPreviousLines)
    {
        var lastLinesText = string.Join("\n", allPreviousLines.ToArray());

        var time = 0f;
        var charIndex = 0;

        while (charIndex < newLine.Length)
        {
            time += Time.deltaTime * TypeWriterSpeed;
            charIndex = Mathf.FloorToInt(time);
            charIndex = Mathf.Clamp(charIndex,0,newLine.Length);

            TextDialogLines.text = newLine.Substring(0, charIndex) + "\n" + lastLinesText;

            yield return null;
        }

        TextDialogLines.text = newLine + "\n" + lastLinesText;
    }
}