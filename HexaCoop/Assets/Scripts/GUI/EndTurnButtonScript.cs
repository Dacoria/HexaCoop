using UnityEngine.UI;

public class EndTurnButtonScript : HexaEventCallback
{
    // Wordt vanuit Button updater & Button event (canvas) geupdatet!

    [ComponentInject] public Button Button;

    public void OnEndTurnButtonClick()
    {
        if (Netw.CurrPlayer().IsOnMyNetwork())
        {
            Netw.CurrPlayer().EndTurn();
        }
    }
}