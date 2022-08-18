using UnityEngine;

public class TooltipGo : MonoBehaviour, ITooltipUIText
{
    public string Content;
    public string Header;

    public void Awake()
    {
        gameObject.AddComponent<TooltipUIHandler>(); // regelt het tonen vd juiste text + gedrag -> via ITooltipUIText
    }
    public string GetContentText() => Content;

    public string GetHeaderText() => Header;
}
