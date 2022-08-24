using UnityEngine.UI;

public class EndTurnButtonScript : HexaEventCallback
{
    // Wordt vanuit Button updater & Button event (canvas) geupdatet!

    [ComponentInject] public Button Button;

    public void OnEndTurnButtonClick()
    {
        if (GameHandler.instance.CurrentPlayer().IsOnMyNetwork())
        {
            NetworkAE.instance.EndPlayerTurn(GameHandler.instance.CurrentPlayer());
        }
    }
}