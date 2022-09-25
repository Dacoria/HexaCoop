public class TemperaryMountainScript : HexaEventCallback, ITurnsLeft
{
    private int TurnActivated;

    [ComponentInject] private MountainScript mountainScript;
    private int TotalTurnsActive = 1;

    public int TurnsLeft => (TurnActivated + TotalTurnsActive + 1) - GameHandler.instance.CurrentTurn; 

    private new void Awake()
    {
        base.Awake();
        TurnActivated = GameHandler.instance.CurrentTurn;
    }

    protected override void OnAllPlayersFinishedTurn()
    {        
        if (TurnsLeft <= 0)
        {
            mountainScript.Destroy();
        }        
    }
}
