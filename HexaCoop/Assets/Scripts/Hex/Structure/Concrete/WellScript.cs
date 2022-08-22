using UnityEngine;

public class WellScript : HexaEventCallback, IStructure
{
    [ComponentInject] private MeshRenderer meshRenderer;
    [ComponentInject] private Hex hex;

    private PlayerScript lastPlayerActivatedWell;
    private int turnLastPlayerActivatedWell;

    private bool wellIsActive;
    private Color originalWellColor;
    private Color orange;


    private new void Awake()
    {
        base.Awake();
        wellIsActive = true;
        originalWellColor = meshRenderer.materials[5].color;
        orange = new Color(214 / 255f, 122 / 255f, 27 / 255f);
        UpdateWellDisplay();
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
            if (turnsLeftTillWellActive <= 0)
            {
                wellIsActive = true;
            }

            UpdateWellDisplay();
        }
    }

    private bool wellIsVisible;
    public void SetIsVisible(bool isVisible) 
    {
        wellIsVisible = isVisible;
        UpdateWellDisplay();
    }

    private void UpdateWellDisplay()
    {
        if(!wellIsVisible)
        {
            SetWellColor(originalWellColor);
            return;
        }

        var turnsLeftTillWellActive = turnLastPlayerActivatedWell + 2 - GameHandler.instance.CurrentTurn; // 2 beurten verder
        if (turnsLeftTillWellActive >= 2)
        {
            SetWellColor(Color.red);
        }
        else if (turnsLeftTillWellActive >= 1)
        {
            SetWellColor(orange);
        }
        else
        {
            SetWellColor(wellIsActive ? Color.green : Color.yellow);
        }        
    } 
      

    // onderkant vd well
    private void SetWellColor(Color color) => meshRenderer.materials[5].color = color;
}
