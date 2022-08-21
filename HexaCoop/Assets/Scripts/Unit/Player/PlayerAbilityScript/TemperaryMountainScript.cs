
using System;
using UnityEngine;

public class TemperaryMountainScript : HexaEventCallback
{
    private int TurnActivated;

    [ComponentInject] private Hex hex;

    public PlayerScript PlayerThatSummonedMountain;

    private new void Awake()
    {
        base.Awake();
        TurnActivated = GameHandler.instance.CurrentTurn;
    }

    protected override void OnEndPlayerTurn(PlayerScript player)
    {
        if (PlayerThatSummonedMountain == player)
        {
            // beurt van activatie + 1 andere beurt actief!
            if (GameHandler.instance.CurrentTurn >= TurnActivated + 1)
            {
                var mountainGo = Utils.GetStructureGoFromHex(hex);

                if (mountainGo != null)
                {
                    var mountainCopy = Instantiate(mountainGo, mountainGo.transform.position, Quaternion.identity);
                    var lerpMovement = mountainCopy.GetSet<LerpMovement>();
                    lerpMovement.MoveDown(distance: 2, duration: 2, destroyGoOnFinished: true);
                }

                hex.ChangeHexStructureType(HexStructureType.None);
            }
        }
    }
}
