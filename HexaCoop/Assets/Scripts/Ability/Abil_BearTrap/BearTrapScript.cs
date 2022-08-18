using System;
using System.Collections.Generic;
using UnityEngine;

public class BearTrapScript : HexaEventCallback
{    
    public BearTrapModel Model; // voor showen/hiden (alleen voor degene waarvan de speler is)

    // geset bij het instantieren
    public PlayerScript PlayerOfTrap;
    public Hex Hex;

    private int turnPlaced;
    private int destroyAfterXTurns = 2;

    private new void Start()
    {
        base.Start();
        if(PlayerOfTrap == null) { throw new Exception("Init vereist voor BearTrap"); }
        gameObject.AddComponent<DestroyBearTrapAfterUse>();
    }

    public void Init(PlayerScript playerOfTrap, Hex hex)
    {
        this.PlayerOfTrap = playerOfTrap;
        this.Hex = hex;

        if (!PlayerOfTrap.IsOnMyNetwork())
        {
            Model.gameObject.SetActive(false);
        }

        turnPlaced = GameHandler.instance.CurrentTurn;
    }

    protected override void OnMovingFinished(PlayerScript playerMoved)
    {
        base.OnMovingFinished(playerMoved);
        if(playerMoved == PlayerOfTrap)
        {
            return;
        }

        if(playerMoved.CurrentHexTile.HexCoordinates == Hex.HexCoordinates)
        {
            ActivateBearTrap(playerMoved);
        }
    }

    protected override void OnEndPlayerTurn(PlayerScript player)
    {
        if(player == PlayerOfTrap)
        {
            if(GameHandler.instance.CurrentTurn >= turnPlaced + destroyAfterXTurns)
            {
                Destroy(gameObject);
            }
        }
    }

    private void ActivateBearTrap(PlayerScript playerOnBeartrap)
    {
        Model.gameObject.SetActive(true);
        Model.GetComponent<Animator>().SetTrigger(Statics.ANIMATION_TRIGGER_ACTIVATE);

        // TODO? -> via animatie laten lopen? Nu: Direct damage via event (goed genoeg)
        NetworkActionEvents.instance.PlayerBeartrapHitPlayer(PlayerOfTrap, Hex, playerOnBeartrap);
    }
}