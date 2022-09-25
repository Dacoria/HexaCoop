
using UnityEngine;

public class TemperaryMountainScript : HexaEventCallback, ITurnsLeft
{
    private int TurnActivated;

    [ComponentInject] private Hex hex;
    private int TotalTurnsActive = 2;

    public PlayerScript PlayerThatSummonedMountain;

    public int TurnsLeft => GameHandler.instance.CurrentTurn - (TurnActivated + TotalTurnsActive + 1);

    private new void Awake()
    {
        base.Awake();
        TurnActivated = GameHandler.instance.CurrentTurn;
    }

    protected override void OnAllPlayersFinishedTurn()
    {        
        if (TurnsLeft <= 0)
        {
            DestroyMountain();
        }        
    }    

    private void DestroyMountain()
    {     
        var mountainGo = Utils.GetStructureGoFromHex(hex);

        if (mountainGo != null)
        {
            var mountainCopy = Instantiate(mountainGo, mountainGo.transform.position, Quaternion.identity);
            var lerpMovement = mountainCopy.GetAdd<LerpMovement>();
            lerpMovement.MoveDown(distance: 2, duration: 2, destroyGoOnFinished: true);
        }

        hex.ChangeHexStructureType(HexStructureType.None);
        Destroy(this);        
    }
}
