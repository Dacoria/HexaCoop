using UnityEngine;
using TMPro;

public class TurnsLeftDisplayScript : HexaEventCallback
{
    [ComponentInject] private ITurnsLeft TurnsLeftComponent;
    [ComponentInject] private TMP_Text Text;

    void Update()
    {
        if (TurnsLeftComponent.Equals(null) || !(TurnsLeftComponent.TurnsLeft > 0))
        {
            Destroy(gameObject);
            return;
        }

        if (Time.frameCount % 10 != 0) return;

        Text.text = TurnsLeftComponent.TurnsLeft.ToString();
    }
}
