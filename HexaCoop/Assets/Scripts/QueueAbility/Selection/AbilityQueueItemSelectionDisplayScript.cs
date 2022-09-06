using UnityEngine;
using UnityEngine.UI;

public class AbilityQueueItemSelectionDisplayScript : MonoBehaviour
{
    [SerializeField] private Image AbilityImage;
    [SerializeField] private Image BackgroundAbility;

    [HideInInspector][ComponentInject] public Button Button;

    private void Awake()
    {
        this.ComponentInject();
    }

    private int QueueItemId;        

    public void SetAbility(int id, AbilityType abilityType, Color colorBg)
    {
        QueueItemId = id;
        AbilityImage.sprite = Rsc.SpriteMap.Get(abilityType.ToString());
        BackgroundAbility.color = colorBg;
    }

    // click on item
    //public void RemoveAbility() => ActionEvents.RemoveQueueItem?.Invoke(Netw.CurrPlayer().GetComponent<PlayerAbilityQueueSelection>().Get(QueueItemId));
    public void RemoveAbility() { }
}
