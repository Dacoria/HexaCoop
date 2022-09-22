using System;
using System.Linq;
using System.Collections;
using UnityEngine;

public class PlayerAiMove : HexaEventCallback
{
    [ComponentInject] private PlayerScript player;    

    public void DoTurn()
    {
        if(GameHandler.instance.GameStatus != GameStatus.PlayerFase)
        {
            return;
        }

        if (player.CurrentAP > 2)
        {
            DoAction();
        }
        else
        {
            NetworkAE.instance.EndPlayerTurn(player);
        }
    }

    private Action currentCallbackAfterAction;
    private Action defaultCallbackAfterAction;

    public void DoAction()
    {
        var randomChoice = UnityEngine.Random.Range(0, 4); // excl. max!
        defaultCallbackAfterAction = DoTurn;

        if (randomChoice == 0 && AbilityType.Rocket.IsAvailable(player))
        {
            DoRocket();
        }
        else if (randomChoice == 1 && AbilityType.Movement.IsAvailable(player))
        {
            DoMovement();
        }        
        else
        {
            // als ability niet kan? retry!
            DoTurn();
        }
    }    

    private void DoRocket()
    {
        var tilesForTarget = HexGrid.instance.GetTiles(HighlightColorType.Blue);

        tilesForTarget = HexGrid.instance.GetAllTiles();        

        tilesForTarget.Shuffle();
        var targetTile = tilesForTarget[0];

        player.Ability(targetTile, AbilityType.Rocket);

        StartCoroutine(WaitThenAction(7));
    }

    private IEnumerator WaitThenAction(float xSeconds)
    {
        yield return Wait4Seconds.Get(xSeconds);

        if(currentCallbackAfterAction != null)
        {
            currentCallbackAfterAction?.Invoke();
            currentCallbackAfterAction = null;
        }
        else
        {
            defaultCallbackAfterAction?.Invoke();
        }
    }

    private void DoMovement()
    {
        var neighbours = HexGrid.instance.GetNeighboursFor(player.CurrentHexTile.HexCoordinates);
        neighbours.Shuffle();
        var targetTile = neighbours[0].GetHex();

        player.Ability(targetTile, AbilityType.Movement);

        StartCoroutine(WaitThenAction(6));
    }   
}
