using UnityEngine;

public class MobileToggleScript : MonoBehaviour
{
    public void OnClick()
    {
        MobileShower.IsShowingMobile = !MobileShower.IsShowingMobile;
        MobileShower.ToggleMobile?.Invoke();
    }
}
