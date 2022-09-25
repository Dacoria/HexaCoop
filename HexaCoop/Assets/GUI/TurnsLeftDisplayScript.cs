using UnityEngine;
using TMPro;

public class TurnsLeftDisplayScript : HexaEventCallback
{
    [ComponentInject(SearchDirection = SearchDirection.PARENT)] private ITurnsLeft TurnsLeftComponent;
    [ComponentInject(SearchDirection = SearchDirection.CHILDREN)] private TMP_Text Text;


    void Update()
    {
        if (Time.frameCount % 10 != 0) return;
        Text.text = TurnsLeftComponent.TurnsLeft.ToString();

        if(TurnsLeftComponent.TurnsLeft == 0)
        {
            Destroy(gameObject);
        }
    }
}
