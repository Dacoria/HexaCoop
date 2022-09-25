using UnityEngine;
using TMPro;

public class TurnsLeftDisplayScript : HexaEventCallback
{
    [ComponentInject] private ITurnsLeft TurnsLeftComponent;
    [ComponentInject] private TMP_Text Text;

    //private void Awake()
    //{
    //    this.ComponentInject();
    //    TurnsLeftComponent = transform.parent.GetComponentInChildren<ITurnsLeft>(); // om transformaties zoals rotatie te voorkomen
    //}

    void Update()
    {
        if (TurnsLeftComponent.Equals(null) || !(TurnsLeftComponent.TurnsLeft > 0))
        {
            Destroy(gameObject);
            return;
        }

        if (Time.frameCount % 10 != 0) return;
        Debug.Log(TurnsLeftComponent.TurnsLeft);

        Text.text = TurnsLeftComponent.TurnsLeft.ToString();
    }
}
