using UnityEngine;

public class WellScript : HexaEventCallback
{
    [ComponentInject] private MeshRenderer meshRenderer;

    private PlayerScript lastPlayerActivatedWell;
    private int turnLastPlayerActivatedWell;

    private bool wellIsActive;   

    private new void Awake()
    {
        base.Awake();
        wellIsActive = true;
        SetWellColor(Color.green);
    }

    public void PlayerSteppedOnStructure(PlayerScript player) => TryUseWell(player);

    private void TryUseWell(PlayerScript player)
    {
        if (wellIsActive)
        {
            player.GetComponent<PlayerHealth>()?.IncreaseHp(1);
            Textt.GameLocal("Well: Gain 1 HP " + wellIsActive, playerFilter: player);

            turnLastPlayerActivatedWell = GameHandler.instance.CurrentTurn;
            lastPlayerActivatedWell = player;
            wellIsActive = false;
            SetWellColor(Color.red);
        }
    }

    protected override void OnEndPlayerTurn(PlayerScript player)
    {
        if(!wellIsActive && player == lastPlayerActivatedWell)
        {
            var turnsLeftTillWellActive = turnLastPlayerActivatedWell + 2 - GameHandler.instance.CurrentTurn; // 2 beurten verder
            if(turnsLeftTillWellActive >= 2)
            {
                SetWellColor(Color.red);
            }
            else if(turnsLeftTillWellActive == 1)
            {
                SetWellColor(Color.yellow);
            }
            else if (turnsLeftTillWellActive <= 0)
            {
                ActivateWell();                
            }
        }
    }

    private void ActivateWell()
    {
        wellIsActive = true;
        SetWellColor(Color.green);
    }    

    // onderkant vd well
    private void SetWellColor(Color color) => meshRenderer.materials[5].color = color;
}
