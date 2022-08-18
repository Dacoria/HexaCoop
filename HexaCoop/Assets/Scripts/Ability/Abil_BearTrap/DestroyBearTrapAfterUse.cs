public class DestroyBearTrapAfterUse : HexaEventCallback
{
    [ComponentInject] private BearTrapScript bearTrapScript;

    private bool BeartrapHasBeenActivated;

    protected override void OnPlayerBeartrapHitPlayer(PlayerScript playerOwnsTrap, Hex hex, PlayerScript playerHit)
    {
        if(hex == bearTrapScript.Hex && playerOwnsTrap == bearTrapScript.PlayerOfTrap)
        {
            BeartrapHasBeenActivated = true;
        }
    }

    protected override void OnEnemyFaseStarted()
    {
        if (BeartrapHasBeenActivated)
        {
            Destroy(gameObject);
        }
    }

    protected override void OnNewPlayerTurn(PlayerScript player)
    {
        if (BeartrapHasBeenActivated)
        {
            Destroy(gameObject);
        }
    }

    protected override void OnEndRound(bool reachedMiddle, PlayerScript pWinner)
    {
        Destroy(gameObject);
    }

    protected override void OnEndGame()
    {
        Destroy(gameObject);
    }    
}