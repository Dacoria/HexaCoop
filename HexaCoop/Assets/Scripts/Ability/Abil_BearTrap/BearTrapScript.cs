using System;
using UnityEngine;

public class BearTrapScript : HexaEventCallback
{    
    public BearTrapModel Model; // voor showen/hiden (alleen voor degene waarvan de speler is)

    // geset bij het instantieren
    public PlayerScript PlayerOfTrap;
    public Hex Hex;

    private int turnPlaced;
    private int destroyAfterXTurns = 2;

    private void Start()
    {
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

    protected override void OnPlayerMovingFinished(PlayerScript playerMoved)
    {
        if(playerMoved.IsOnMyNetwork() && playerMoved.CurrentHexTile.HexCoordinates == Hex.HexCoordinates)
        {
            ActivateBearTrap(playerMoved);
        }
    }    

    private void ActivateBearTrap(PlayerScript playerOnBeartrap)
    {
        Model.gameObject.SetActive(true);
        Model.GetComponent<Animator>().SetTrigger(Statics.ANIMATION_TRIGGER_ACTIVATE);

        NetworkAE.instance.PlayerBeartrapHitPlayer(PlayerOfTrap, Hex, playerOnBeartrap);
    }

    protected override void OnEndPlayerTurn(PlayerScript player)
    {
        if (player == PlayerOfTrap)
        {
            if (GameHandler.instance.CurrentTurn >= turnPlaced + destroyAfterXTurns)
            {
                Destroy(gameObject);
            }
        }
    }
}